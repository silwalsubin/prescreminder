import { InjectionKey } from 'vue';
import { createStore, useStore as baseUseStore, Store, MutationTree } from 'vuex';
import httpClient from '@/store/http-client';
import { removeBearerToken, setBearerToken } from '@/bearer-token-service';
import AddPrescriptionPayload from './payloads/add-prescription-payload';
import PrescriptionViewModal from './view-models/prescription-view-modal';
import _ from 'lodash';


export interface State {
  addPrescriptionPayload: AddPrescriptionPayload;
  prescriptions: PrescriptionViewModal[];
}

export const key: InjectionKey<Store<State>> = Symbol()

export const store = createStore<State>({
  state: {
    addPrescriptionPayload: new AddPrescriptionPayload(),
    prescriptions: []
  },
  actions: {
    logIn(_, payload) {
      return httpClient.post('/user/login', payload).then(response => {
        const bearerToken = response.data.token;
        setBearerToken(bearerToken);
      })
    },
    loadPrescriptions({commit}) {
      return httpClient.get('/userPrescription').then(response => {
        commit('setPrescriptions', response.data);
      })
    },
    addPrescription({dispatch}, payload) {
      return httpClient.post('/userPrescription/add', payload).then(() => {
        dispatch('loadPrescriptions');
      });
    },
    logOut() {
      removeBearerToken();
    }
  },
  mutations: {
    setPrescriptions(state, payload){
      state.prescriptions = payload;
    }
  },
  getters: {
    addPrescriptionPayload: state => _.cloneDeep(state.addPrescriptionPayload),
    prescriptions: state => state.prescriptions
  }
})

// define your own `useStore` composition function
export function useStore () {
  return baseUseStore(key)
}