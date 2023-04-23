using MailKit.Net.Smtp;
using MimeKit;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class EmailService
    {
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
                    client.Connect("");
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
            
            emailMensagem.From.Add(new MailboxAddress(""));
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
