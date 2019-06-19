using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Inexa.Atlantis.Re.Commons.Infras.Configuration;
using Inexa.Atlantis.Re.Commons.Infras.Domains;
using Inexa.Atlantis.Re.Commons.Infras.Enums;
using Inexa.Atlantis.Re.Commons.Infras.Logging;

namespace Inexa.Atlantis.Re.Commons.Infras.Helpers
{
    public static class Utilities
    {
        private static DateTime _currentDate = DateTime.Now;

        public static DateTime CurrentDate
        {
            get
            {
                return DateTime.Now;
            }
            set
            {
                _currentDate = value;
            }
        }

        public static int GetAge(this DateTime birthDay)
        {
            if (birthDay == null)
            {
                throw new Exception("la date ne peut etre null");
            }
            return CurrentDate.Year - birthDay.Year;
        }
        public static bool HasSpecialCharacters(this string str)
        {
            const string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            var specialCharactersArray = specialCharacters.ToCharArray();

            var index = str.IndexOfAny(specialCharactersArray);

            return index != -1;
        }
    }

    /// <span class="code-SummaryComment"><summary/></span>
    /// Provides a method for performing a deep copy of an object.
    /// Binary Serialization is used to perform the copy.
    /// <span class="code-SummaryComment"></span>
    public static class ObjectCopier
    {
        /// <span class="code-SummaryComment"><summary/></span>
        /// Perform a deep Copy of the object.
        /// <span class="code-SummaryComment"></span>
        /// <span class="code-SummaryComment"><typeparam name="T">The type of object being copied.</typeparam></span>
        /// <span class="code-SummaryComment"><param name="source">The object instance to copy.</param></span>
        /// <span class="code-SummaryComment"><returns>The copied object.</returns></span>
        public static T Clone<T>(this T source)
        {
            var dcSer = new DataContractSerializer(source.GetType());
            var memoryStream = new MemoryStream();

            dcSer.WriteObject(memoryStream, source);
            memoryStream.Position = 0;

            var newObject = (T)dcSer.ReadObject(memoryStream);
            return newObject;
        }
    }

    /// <summary>
    /// Represente les erreurs qui se produisent lors de l'execution de l'application
    /// </summary>
    public class CustomException : Exception
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe CustomException avec un message d'erreur specifié
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exceptionType"></param>
        public CustomException(string message, string exceptionType = null)
            : base(Processor(message, exceptionType))
        {
        }


        /// <summary>
        /// Capture une exception et envoi un email contenant les details de l'erreur à l'administrateur de l'application
        /// </summary>
        /// <param name="request">Contient les parametres d'entree ayant generé l'exception</param>
        /// <param name="response">Contient l'exception generée</param>
        /// <param name="sendMail"></param>
        public CustomException(RequestBase request, ResponseBase response, bool sendMail = false)
            : base(Processor(request, response, sendMail))
        {
        }

        /// <summary>
        /// Notifie une exception 
        /// </summary>
        /// /// <param name="response"></param>
        /// <param name="ex"></param>
        public static void Write(ResponseBase response, Exception ex)
        {
            response.Message = GetErrorMessage(ex);
            response.HasError = true;
        }

        /// <summary>
        /// Notifie une exception 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="ex"></param>
        public static void Write(RequestBase request, ResponseBase response, Exception ex)
        {
            response.Message = GetErrorMessage(ex);
            response.HasError = true;
        }

        public static string GetErrorMessage(Exception ex)
        {
            var listException = ex.FlattenHierarchy().ToList();
            var msg = string.Empty;
            var level = 0;
            foreach (var exception in listException)
            {
                if (exception.Message.EndsWith(GlobalConstantes.CustomException) || exception.Message.StartsWith("Level 1"))
                {
                    msg = exception.Message;
                }
                else
                {
                    level++;
                    msg = msg + string.Format("Level {0}: {1}<br/>", level, exception.Message);
                }
            }

            return msg;
        }

        private static string Processor(string message, string exceptionType = null)
        {
            return string.Format("{0} {1}", message, exceptionType ?? GlobalConstantes.CustomException);
        }

        private static string Processor(RequestBase request, ResponseBase response, bool sendMail = false)
        {
            var webConfigApp = new WebConfigApplicationSettings();

            var detailException = string.Format("REQUEST :\n {0}.\n\n\n RESPONSE :\n{1}", request.ToXmlString(),
                                                response.ToXmlString());
            var mailBody =
                string.Format("exception : {0} \n\n detail exception : {1}",
                              response.Message, detailException);

            EmailSender.SendAsync(new MailRequest()
            {
                Sender = webConfigApp.ExpEmail,
                SenderName = webConfigApp.ExpFullName,
                Recipient = webConfigApp.ToMail,
                RecipientName = webConfigApp.ToFullName,
                Subject = @"ERP Exception",
                Body = mailBody,
                SendOrNo = sendMail,
                BccRecipient = webConfigApp.BccMail,
                BccRecipientName = webConfigApp.BccFullName,
                IsBodyHtml = true
            });

            LoggingFactory.GetLogger().Log(string.Format("Utilities => ThrowException : exception {0}", mailBody));

            return response.Message;
        }
    }

    public static class DebugExtensions
    {
        public static IEnumerable<Exception> FlattenHierarchy(this Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException("ex");
            }
            var innerException = ex;
            do
            {
                yield return innerException;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }
    }

    public static class EmailHelper
    {
        /// <summary>
        /// Regular expression, which is used to validate an E-Mail address.
        /// </summary>
        private const string MatchEmailPattern =
                  @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        /// <summary>
        /// Verifier si la chaine est une adresse E-Mail valide.
        /// </summary>
        /// <param name="email">chaine qui contient l'adresse E-Mail.</param>
        /// <returns>True, quand la chaine est non null et 
        /// contient une adresse E-Mail valide;
        /// sinon false.</returns>
        public static bool IsEmail(this string email)
        {
            return email != null && Regex.IsMatch(email, MatchEmailPattern);
        }
    }

    public class ConverterHelper
    {
        public static string ConvertNumberToLettre(float number)
        {
            int centaine, dizaine, unite, reste, y;
            var dix = false;
            var lettre = string.Empty;


            reste = (int)(number / 1);

            for (var i = 1000000000; i >= 1; i /= 1000)
            {
                y = reste / i;
                if (y != 0)
                {
                    centaine = y / 100;
                    dizaine = (y - centaine * 100) / 10;
                    unite = y - (centaine * 100) - (dizaine * 10);
                    switch (centaine)
                    {
                        case 0:
                            break;
                        case 1:
                            lettre += "cent ";
                            break;
                        case 2:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "deux cents ";
                            }
                            else
                            {
                                lettre += "deux cent ";
                            }
                            break;
                        case 3:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "trois cents ";
                            }
                            else
                            {
                                lettre += "trois cent ";
                            }
                            break;
                        case 4:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "quatre cents ";
                            }
                            else
                            {
                                lettre += "quatre cent ";
                            }
                            break;
                        case 5:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "cinq cents ";
                            }
                            else
                            {
                                lettre += "cinq cent ";
                            }
                            break;
                        case 6:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "six cents ";
                            }
                            else
                            {
                                lettre += "six cent ";
                            }
                            break;
                        case 7:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "sept cents ";
                            }
                            else
                            {
                                lettre += "sept cent ";
                            }
                            break;
                        case 8:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "huit cents ";
                            }
                            else
                            {
                                lettre += "huit cent ";
                            }
                            break;
                        case 9:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "neuf cents ";
                            }
                            else
                            {
                                lettre += "neuf cent ";
                            }
                            break;
                    }

                    switch (dizaine)
                    {
                        case 0:
                            break;
                        case 1:
                            dix = true;
                            break;
                        case 2:
                            lettre += "vingt ";
                            break;
                        case 3:
                            lettre += "trente ";
                            break;
                        case 4:
                            lettre += "quarante ";
                            break;
                        case 5:
                            lettre += "cinquante ";
                            break;
                        case 6:
                            lettre += "soixante ";
                            break;
                        case 7:
                            dix = true;
                            lettre += "soixante ";
                            break;
                        case 8:
                            lettre += "quatre-vingt ";
                            break;
                        case 9:
                            dix = true;
                            lettre += "quatre-vingt ";
                            break;
                    }

                    switch (unite)
                    {
                        case 0:
                            if (dix)
                            {
                                lettre += "dix ";
                            }
                            break;
                        case 1:
                            if (dix)
                            {
                                lettre += "onze ";
                            }
                            else
                            {
                                lettre += "un ";
                            }
                            break;
                        case 2:
                            if (dix)
                            {
                                lettre += "douze ";
                            }
                            else
                            {
                                lettre += "deux ";
                            }
                            break;
                        case 3:
                            if (dix)
                            {
                                lettre += "treize ";
                            }
                            else
                            {
                                lettre += "trois ";
                            }
                            break;
                        case 4:
                            if (dix)
                            {
                                lettre += "quatorze ";
                            }
                            else
                            {
                                lettre += "quatre ";
                            }
                            break;
                        case 5:
                            if (dix)
                            {
                                lettre += "quinze ";
                            }
                            else
                            {
                                lettre += "cinq ";
                            }
                            break;
                        case 6:
                            if (dix)
                            {
                                lettre += "seize ";
                            }
                            else
                            {
                                lettre += "six ";
                            }
                            break;
                        case 7:
                            if (dix)
                            {
                                lettre += "dix-sept ";
                            }
                            else
                            {
                                lettre += "sept ";
                            }
                            break;
                        case 8:
                            if (dix)
                            {
                                lettre += "dix-huit ";
                            }
                            else
                            {
                                lettre += "huit ";
                            }
                            break;
                        case 9:
                            if (dix)
                            {
                                lettre += "dix-neuf ";
                            }
                            else
                            {
                                lettre += "neuf ";
                            }
                            break;
                    }

                    switch (i)
                    {
                        case 1000000000:
                            if (y > 1)
                            {
                                lettre += "milliards ";
                            }
                            else
                            {
                                lettre += "milliard ";
                            }
                            break;
                        case 1000000:
                            if (y > 1)
                            {
                                lettre += "millions ";
                            }
                            else
                            {
                                lettre += "million ";
                            }
                            break;
                        case 1000:
                            lettre += "mille ";
                            break;
                    }
                }
                reste -= y * i;
                dix = false;
            }
            if (lettre.Length == 0)
            {
                lettre += "zero";
            }
            return lettre;
        }

        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return Convert.ChangeType(value, t);
        }


        public static LambdaExpression ChangeInputType<T, TResult>(Expression<Func<T, TResult>> expression, Type newInputType)
        {
            if (!typeof(T).IsAssignableFrom(newInputType))
            {
                throw new Exception(string.Format("{0} is not assignable from {1}.", typeof(T), newInputType));
            }
            var beforeParameter = expression.Parameters.Single();
            var afterParameter = Expression.Parameter(newInputType, beforeParameter.Name);
            var visitor = new SubstitutionExpressionVisitor(beforeParameter, afterParameter);
            return Expression.Lambda(visitor.Visit(expression.Body), afterParameter);
        }

        public static Expression<Func<T2, TResult>> ChangeInputType<T1, T2, TResult>(Expression<Func<T1, TResult>> expression)
        {
            if (!typeof(T1).IsAssignableFrom(typeof(T2)))
            {
                throw new Exception(string.Format("{0} is not assignable from {1}.", typeof(T1), typeof(T2)));
            }
            var beforeParameter = expression.Parameters.Single();
            var afterParameter = Expression.Parameter(typeof(T2), beforeParameter.Name);
            var visitor = new SubstitutionExpressionVisitor(beforeParameter, afterParameter);
            return Expression.Lambda<Func<T2, TResult>>(visitor.Visit(expression.Body), afterParameter);
        }

        public class SubstitutionExpressionVisitor : ExpressionVisitor
        {
            private Expression before, after;
            public SubstitutionExpressionVisitor(Expression before, Expression after)
            {
                this.before = before;
                this.after = after;
            }
            public override Expression Visit(Expression node)
            {
                return node == before ? after : base.Visit(node);
            }
        }
    }
}
