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
        console.log(response.data);
        commit('setPrescriptions', response.data);
      })
    },
    addPrescription({dispatch}, payload) {
      return httpClient.post('/userPrescription/add', payload).then(() => {
        dispatch('loadPrescriptions');
      });
    },
    deletePrescription({commit}, prescriptionId) {
      return httpClient.delete(`/userPrescription/${prescriptionId}`).then(() => {
        commit('deletePrescription', prescriptionId);
      })
    },
    updatePrescription({commit}, payload){
      return httpClient.post(`/userPrescription/${payload.prescriptionId}`, payload.viewModal).then(() => {
        commit('updatePrescription', payload.viewModal);
      })
    },
    logOut() {
      removeBearerToken();
    }
  },
  mutations: {
    setPrescriptions(state, payload){
      state.prescriptions = payload;
    },
    deletePrescription(state, prescriptionId){
      const index = state.prescriptions.findIndex(x => x.prescriptionId === prescriptionId);
      state.prescriptions.splice(index, 1);
    },
    updatePrescription(state, payload){
      const index = state.prescriptions.findIndex(x => x.prescriptionId === payload.prescriptionId);
      state.prescriptions[index] = payload;
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