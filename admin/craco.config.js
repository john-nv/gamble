const { when, whenDev, whenProd, whenCI, whenTest, ESLINT_MODES, POSTCSS_MODES } = require('@craco/craco');
const CracoAlias = require('craco-alias')

module.exports = {
  eslint: {
    enable: false
  },
  plugins: [
    {
      plugin: CracoAlias,
      options: {
        source: "jsconfig",
        baseUrl: "./src/",
        tsConfigPath: "./jsconfig.extend.json"
      }
    }
  ],
  webpack: {
    alias: {},
    plugins: [],
    configure: (webpackConfig, { env, paths }) => {
      if (!webpackConfig.plugins) {
        config.plugins = [];
      }

      return webpackConfig
    }
  }
}