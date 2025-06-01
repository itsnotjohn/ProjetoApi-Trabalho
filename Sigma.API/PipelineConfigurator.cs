namespace Sigma.API
{
    public class PipelineConfigurator
    {
        private readonly WebApplication _app;

        public PipelineConfigurator(WebApplication app) => _app = app;

        public void ConfigurePipeline()
        {
            ConfigureSwagger();
            ConfigureSecurity();
            ActiveArchives();
            MapControllers();
        }

        private void ConfigureSwagger()
        {
            if (_app.Environment.IsDevelopment())
            {
                _app.UseSwagger();
                _app.UseSwaggerUI();
            }
        }

        private void ConfigureSecurity()
        {
            _app.UseAuthentication();
            _app.UseAuthorization();
        }

        public void ActiveArchives()
		{
			_app.UseStaticFiles();
		}

		private void MapControllers()
        {
            _app.MapControllers();
        }
    }
}
