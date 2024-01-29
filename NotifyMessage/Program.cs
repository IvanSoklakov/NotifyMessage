using NLog.Web;
using NotifyMessage.BuisnessLogicLayer.Infrastucture;
using NotifyMessage.BuisnessLogicLayer.Interfaces;
using NotifyMessage.BuisnessLogicLayer.Services;


var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

try
{
    #region ConfigureServices
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddAutoMapper(typeof(MappingProfile));
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddTransient<IMessageServices, MessageServices>();
    #endregion

    #region Configure
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    #endregion

}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}