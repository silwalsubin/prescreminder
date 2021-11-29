import axios, { AxiosRequestConfig } from 'axios'
import { notifyAsync, NotificationType } from '@/toast-notifications'
import { getBearerToken, removeBearerToken } from '../bearer-token-service'

const httpClient = axios;
// httpClient.defaults.baseURL = "https://localhost:44340/api"
httpClient.defaults.baseURL = "https://prescreminder.azurewebsites.net/api"
httpClient.interceptors.response.use((response) => {
  return response;
}, async error => {
  // Do something with response error
  const waitTime = 4000;
  if (error.response.status === 401) {
    await notifyAsync(NotificationType.Warning, 'Session Expired!');

    setTimeout(() => {
      removeBearerToken();
      window.location.href = `${window.location.origin}`;
    }, waitTime);
  }

  if (error.response.status === 400) {
    await notifyAsync(NotificationType.Error, error.response.data);
  }
  return Promise.reject(error.response);
});

const injectBearerToken = (config: AxiosRequestConfig): AxiosRequestConfig => {
    const bearerToken = getBearerToken();
    if (bearerToken && config.headers) {
      config.headers.Authorization = `Bearer ${bearerToken}`;
    }
    return config;
}

httpClient.interceptors.request.use(injectBearerToken, error => {
  Promise.reject(error)
})
export default httpClient;

