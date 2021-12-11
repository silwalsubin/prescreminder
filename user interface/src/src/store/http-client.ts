import axios, { AxiosRequestConfig, AxiosResponse } from 'axios'
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

const injectUserTimeZone = (config: AxiosRequestConfig): AxiosRequestConfig => {
  if (config.headers) {
    config.headers["timeZone"] = Intl.DateTimeFormat().resolvedOptions().timeZone;
  }
  return config;
}

httpClient.interceptors.request.use(injectBearerToken, error => {
  Promise.reject(error)
})

httpClient.interceptors.request.use(injectUserTimeZone, error => {
  Promise.reject(error)
})
export default httpClient;

export const fileDownload = (response: AxiosResponse): void => {
  const fileName = response.headers['content-disposition'].split('filename=')[1].split(';')[0];
  const url = window.URL.createObjectURL(new Blob([response.data]));
  const link = document.createElement('a');
  link.href = url;
  link.setAttribute('download', fileName);
  document.body.appendChild(link);
  link.click();
}

