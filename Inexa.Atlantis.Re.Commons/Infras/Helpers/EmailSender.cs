using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using MailMessage = System.Net.Mail.MailMessage;

namespace Inexa.Atlantis.Re.Commons.Infras.Helpers
{
    public static class EmailSender
    {
        /// <summary>
        /// Sends an Email.
        /// </summary>
        /// <param name="request">contient les proprietes d'envoie de mail</param>
        /// <returns>retourne True si l'envoi à réussie</returns>
        public static bool Send(MailRequest request)
        {
            if (!request.SendOrNo)
            {
                return false;
            }
            var message = new MailMessage()
            {
                From = new MailAddress(request.Sender, request.SenderName),
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = request.IsBodyHtml
            };
            message.To.Add(new MailAddress(request.Recipient, request.RecipientName));
            if (!string.IsNullOrEmpty(request.BccRecipient) && !string.IsNullOrEmpty(request.BccRecipientName))
            {
                message.Bcc.Add(new MailAddress(request.BccRecipient, request.BccRecipientName));
            }

            try
            {
                var client = new SmtpClient();
                client.Send(message);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sends an Email asynchronously
        /// </summary>
        /// <param name="request">contient les proprietes d'envoie de mail</param>
        public static void SendAsync(MailRequest request)
        {
            if (!request.SendOrNo)
            {
                return;
            }
            var message = new MailMessage()
            {
                From = new MailAddress(request.Sender, request.SenderName),
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = request.IsBodyHtml
            };

            message.To.Add(new MailAddress(request.Recipient, request.RecipientName));

            if (!string.IsNullOrEmpty(request.BccRecipient) && !string.IsNullOrEmpty(request.BccRecipientName))
            {
                message.Bcc.Add(new MailAddress(request.BccRecipient, request.BccRecipientName));
            }
            if (request.Attachments != null && request.Attachments.Any())
            {
                foreach (var mailAttachment in request.Attachments)
                {
                    message.Attachments.Add(mailAttachment);
                }
            }
            var client = new SmtpClient();

            Task.Factory.StartNew(() =>
            {
                var sendTask = client.SendCustAsync(message);
                sendTask.ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Exception ex = task.Exception;

                        throw ex ?? new Exception("Erreur envoi de mail");
                    }
                    else
                    {
                        if (task.IsCanceled)
                        {
                        }
                        else
                        {
                        }
                    }
                });

            });
        }

        private static Task SendCustAsync(this SmtpClient client, MailMessage message)
        {
            var tcs = new TaskCompletionSource<object>();
            var sendGuid = Guid.NewGuid();

            SendCompletedEventHandler handler = null;
            handler = (o, ea) =>
            {
                if (ea.UserState is Guid && ((Guid) ea.UserState) == sendGuid)
                {
                    client.SendCompleted -= handler;
                    if (ea.Cancelled)
                    {
                        tcs.SetCanceled();
                        throw new Exception("Envoi de mail annulé");
                    }
                    if (ea.Error != null)
                    {
                        tcs.SetException(ea.Error);
                        throw ea.Error;
                    }
                    tcs.SetResult(null);
                }
            };

            client.SendCompleted += handler;
            client.SendAsync(message, sendGuid);

            return tcs.Task;
        }
    }

    /// <summary>
    /// Parametre d'envoie de mail
    /// </summary>
    public class MailRequest
    {
        /// <summary>
        /// Mail de la source
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Nom de la source
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Mail du destinataire
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Nom du destinataire
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// Objet
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Corps du mail
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Doit envoyer le mail ou non
        /// </summary>
        public bool SendOrNo { get; set; }

        /// <summary>
        /// Mail en copy caché
        /// </summary>
        public string BccRecipient { get; set; }

        /// <summary>
        /// Nom du destinataire en copy caché
        /// </summary>
        public string BccRecipientName { get; set; }

        /// <summary>
        /// Set mail body in html format
        /// </summary>
        public bool IsBodyHtml { get; set; }

        public List<System.Net.Mail.Attachment> Attachments { get; set; }

        public MailRequest()
        {
            BccRecipient = string.Empty;
            BccRecipientName = string.Empty;
            IsBodyHtml = false;
        }
    }
}
