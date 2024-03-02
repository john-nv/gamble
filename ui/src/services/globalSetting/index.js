const { default: http } = require("@services/http");

const getApplicationConfiguration = () =>
  http.get('api/abp/application-configuration')

const getApplicationLocalization = (options = null) =>
  http.get('api/abp/application-localization', {
    params: {
      cultureName: options?.cultureName ?? 'vi',
      onlyDynamics: options?.onlyDynamics ?? true
    }
  })

export default {
  getApplicationConfiguration,
  getApplicationLocalization
}