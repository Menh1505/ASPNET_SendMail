using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions();
var mailsettings = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailsettings);
builder.Services.AddTransient<SendMailService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/TestSendMail", async (context) => {
    var message = await MailUtils.MailUtils.SendMail("dinhthienmenh1505@gmail.com", "dinhthienmenh1505@gmail.com", "test", "hello world!");
    context.Response.WriteAsync(message);
});
app.MapGet("/TestGmail", async (context) => {
    var message = await MailUtils.MailUtils.SendGmail("dinhthienmenh1505@gmail.com", "dinhthienmenh1505@gmail.com", "test", "hello world!", "dinhthienmenh1505@gmail.com", "rrhv ihmt odjt rgia");
    context.Response.WriteAsync(message);
});
app.MapGet("/TestSendMailService", async (context) => {
    var sendMailService = context.RequestServices.GetService<SendMailService>();
    var mailContent = new MailContent();
    
    mailContent.To = "dinhthienmenh1505@gmail.com";
    mailContent.Subject = "KIEM TRA THU EMAIL";
    mailContent.Body = "<h1>TEST</h1><i>Hello World!</i>";
    
    var kq = await sendMailService.SendMail(mailContent);

    await context.Response.WriteAsync(kq);
});
app.Run();

