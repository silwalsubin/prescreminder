import { InjectionKey } from 'vue';
import { createStore, useStore as baseUseStore, Store } from 'vuex';
import httpClient from './http-client';
import { removeBearerToken, setBearerToken } from '../bearer-token-service';
import AddPrescriptionPayload from './payloads/add-prescription-payload';
import _ from 'lodash';


export interface State {
  addPrescriptionPayload: AddPrescriptionPayload;
}

export const key: InjectionKey<Store<State>> = Symbol()

export const store = createStore<State>({
  state: {
    addPrescriptionPayload: new AddPrescriptionPayload()
  },
  actions: {
    logIn(_, payload) {
      return httpClient.post('/user/login', payload).then(response => {
        const bearerToken = response.data.token;
        setBearerToken(bearerToken);
      })
    },
    addPrescription(_, payload) {
      return httpClient.post('/userPrescription/add', payload).then(response => {
        console.log(response);
      })
    },
    logOut() {
      removeBearerToken();
    }
  },
  getters: {
    addPrescriptionPayload: state => _.cloneDeep(state.addPrescriptionPayload)
  }
})

// define your own `useStore` composition function
export function useStore () {
  return baseUseStore(key)
}