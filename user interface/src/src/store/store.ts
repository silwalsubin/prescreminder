import { InjectionKey } from 'vue'
import { createStore, useStore as baseUseStore, Store } from 'vuex'
import httpClient from './http-client'
import { removeBearerToken, setBearerToken } from '../bearer-token-service'

export interface State {
  count: number;
}

export const key: InjectionKey<Store<State>> = Symbol()

export const store = createStore<State>({
  actions: {
    logIn(_, payload) {
      return httpClient.post('/user/login', payload).then(response => {
        const bearerToken = response.data.token;
        setBearerToken(bearerToken);
      })
    },
    logOut() {
      removeBearerToken();
    }
  }
})

// define your own `useStore` composition function
export function useStore () {
  return baseUseStore(key)
}