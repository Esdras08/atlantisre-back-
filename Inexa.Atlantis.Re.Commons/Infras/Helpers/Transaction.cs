using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Transactions;
using Inexa.Atlantis.Re.Commons.Infras.Domains;

namespace Inexa.Atlantis.Re.Commons.Infras.Helpers
{
    public static class TransactionFactory
    {
        public delegate ResponseBase MethodBase(RequestBase request);

        public static ResponseBase ApplyTransaction(RequestBase request, MethodBase method)
        {
            if (!request.CanApplyTransaction)
            {
                return method(request);
            }
            var transaction = Create();
            var response = new ResponseBase();
            try
            {
                using (transaction)
                {
                    response = method(request);
                    if (response.HasError)
                    {
                        throw new CustomException(request, response, false);
                    }
                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                response.HasError = true;
            }
            finally
            {
                transaction.Dispose();
            }

            return response;
        }

        public static TransactionScope Create()
        {
            return new TransactionScope(TransactionScopeOption.RequiresNew,
                                             new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted, Timeout = TimeSpan.FromSeconds(60) });
        }
    }

    public static class TransactionQueueManager
    {
        /// <summary>
        /// Liste des files de toutes les transactions
        /// </summary>
        private static List<TransactionQueue> _queue = new List<TransactionQueue>();

        /// <summary>
        /// File d'une transaction
        /// </summary>
        private class TransactionQueue
        {
            public TransactionScope Transaction { get; set; }
            public int ProcessId { get; set; }
            public int ProcessDeepth { get; set; }

            private void Terminate()
            {
                Transaction.Dispose();
                ProcessId = 0;
                ProcessDeepth = 0;
            }

            public void Rollback()
            {
                Terminate();
            }

            public void Commit()
            {
                Transaction.Complete();
                Terminate();
            }
        }

        private static TransactionQueue GetCurrentTransactionQueue()
        {
            var processId = Thread.CurrentThread.ManagedThreadId;
            return _queue.FirstOrDefault(c => c.ProcessId == processId);
        }

        private static void SetProcessDeepth()
        {
            if (IsTransactionStarted())
            {
                GetCurrentTransactionQueue().ProcessDeepth++;
            }
        }

        private static void DeleteProcessDeepth()
        {
            if (IsTransactionStarted())
            {
                GetCurrentTransactionQueue().ProcessDeepth--;
            }
        }

        private static void StartTransaction()
        {
            if (IsTransactionStarted())
            {
                throw new Exception("Deux (2) transactions ne peuvent être gérées à la fois dans le même processus!");
            }
            var queue = new TransactionQueue { ProcessId = Thread.CurrentThread.ManagedThreadId, ProcessDeepth = 1, Transaction = TransactionFactory.Create() };
            _queue.Add(queue);
        }

        private static bool IsTransactionStarted()
        {
            return GetCurrentTransactionQueue() != null;
        }

        private static void KillCurrentTransaction()
        {
            if (!IsTransactionStarted())
            {
                return;
            }
            var transactionQueue = GetCurrentTransactionQueue();
            if (transactionQueue != null)
            {
                _queue.Remove(transactionQueue);
            }
        }

        public static void BeginWork(RequestBase request, ResponseBase response)
        {
            SetProcessDeepth();

            if (!request.CanApplyTransaction || IsTransactionStarted())
            {
                return;
            }
            StartTransaction();


            request.CanApplyTransaction = false;
        }

        public static void FinishWork(RequestBase request, ResponseBase response)
        {
            DeleteProcessDeepth();

            if (IsTransactionStarted() && GetCurrentTransactionQueue().ProcessDeepth == 0 && !response.HasError)
            {
                var transactionQueue = GetCurrentTransactionQueue();
                if (transactionQueue != null)
                {
                    transactionQueue.Commit();
                }
                KillCurrentTransaction();
                return;
            }

            if (!IsTransactionStarted() || !response.HasError)
            {
                return;
            }
            var transactionQueueForRollback = GetCurrentTransactionQueue();
            if (transactionQueueForRollback != null)
            {
                transactionQueueForRollback.Rollback();
            }
            KillCurrentTransaction();
        }
    }
}
