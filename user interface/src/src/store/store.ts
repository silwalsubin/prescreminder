import { InjectionKey } from 'vue'
import { createStore, useStore as baseUseStore, Store } from 'vuex'
import httpClient from './http-client'
import Cookies from 'cookies-ts';
const BEARER_TOKEN_COOKIE_KEY = "access_token";
const cookies = new Cookies();


export interface State {
  count: number;
}

export const key: InjectionKey<Store<State>> = Symbol()

export const store = createStore<State>({
  actions: {
    logIn(_, payload) {
      return httpClient.post('/user/login', payload).then(response => {
        const bearerToken = response.data.token;
        cookies.set(BEARER_TOKEN_COOKIE_KEY, bearerToken);
      })
    },
    logOut() {
      cookies.remove(BEARER_TOKEN_COOKIE_KEY);
    }
  }
})

// define your own `useStore` composition function
export function useStore () {
  return baseUseStore(key)
}