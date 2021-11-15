import axios, { AxiosRequestConfig } from 'axios'
import { toastController } from '@ionic/vue';
import { getBearerToken } from '../bearer-token-service'

const httpClient = axios;
httpClient.defaults.baseURL = "https://localhost:44340/api"
// httpClient.defaults.baseURL = "https://prescreminder.azurewebsites.net/api"
httpClient.interceptors.response.use((response) => {
  return response;
}, async error => {
  // Do something with response error
  if (error.response.status === 401) {
      console.log('unauthorized, logging out ...');
  }

  if (error.response.status === 400) {
    const toast = await toastController.create({
      message: error.response.data,
      duration: 2000, 
      color: "danger",
      animated: true
    });
    toast.present();
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

