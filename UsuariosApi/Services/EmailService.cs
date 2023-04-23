using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EnviarEmail(string[] destinatarios, string assunto, int usuarioId, string code)
        {
            Mensagem mensagem = new Mensagem(destinatarios, assunto, usuarioId, code);

            var mensagemDeEmail = CriaCorpoDoEmail(mensagem);
            Enviar(mensagemDeEmail);
        }

        private void Enviar(MimeMessage mensagemDeEmail)
        {
            using(var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"),
                        _configuration.GetValue<int>("EmailSettings:Port"), MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);

                    client.AuthenticationMechanisms.Remove("XOUATH2");

                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
                        _configuration.GetValue<string>("EmailSettings:Password"));

                    client.Send(mensagemDeEmail);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CriaCorpoDoEmail(Mensagem mensagem)
        {
            var emailMensagem = new MimeMessage();
            
            emailMensagem.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:From")));
            emailMensagem.To.AddRange(mensagem.Destinatario);
            emailMensagem.Subject = mensagem.Assunto;
            emailMensagem.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            { 
                Text = mensagem.Conteudo 
            };

            return emailMensagem;
        }
    }
}
