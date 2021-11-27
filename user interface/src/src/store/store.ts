import { InjectionKey } from 'vue';
import { createStore, useStore as baseUseStore, Store } from 'vuex';
import httpClient from '@/store/http-client';
import { removeBearerToken, setBearerToken } from '@/bearer-token-service';
import AddPrescriptionPayload from './payloads/add-prescription-payload';
import PrescriptionViewModal from './view-models/prescription-view-modal';
import MedicationInfoViewModel from './view-models/medication-info-view-model';
import UserHistoryViewModel from './view-models/user-history-view-model';
import MedicationCheckListItem from './view-models/medication-check-list-item';
import _ from 'lodash';


export interface State {
  addPrescriptionPayload: AddPrescriptionPayload;
  prescriptions: PrescriptionViewModal[];
  medicationsToday: MedicationInfoViewModel[];
  userHistories: UserHistoryViewModel[];
}

export const key: InjectionKey<Store<State>> = Symbol()

export const store = createStore<State>({
  state: {
    addPrescriptionPayload: new AddPrescriptionPayload(),
    prescriptions: [],
    medicationsToday: [],
    userHistories: [],
  },
  actions: {
    logIn(_, payload) {
      return httpClient.post('/user/login', payload).then(response => {
        const bearerToken = response.data.token;
        setBearerToken(bearerToken);
      })
    },
    loadMedicationsToday({commit}){
      return httpClient.get('/userMedicationToday').then(response => {
        commit('updateMedicationsToday', response.data);
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
    updateMedicationTaken({commit}, payload){
      commit('updateMedicationTaken', payload);
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
    },
    updateMedicationsToday(state, payload){
      state.medicationsToday = payload;
    },
    updateMedicationTaken(state, payload){
      const index = state.userHistories.findIndex(y => 
        y.name === payload.name &&
        y.quantity === payload.quantity &&
        y.hour === payload.hour && 
        y.minute === payload.minute
      );

      if (index === -1)
      {
        state.userHistories.push(new UserHistoryViewModel(
          payload.name, 
          payload.quantity,
          payload.hour,
          payload.minute
        ))
      } else {
        state.userHistories.splice(index, 1);
      }
    }
  },
  getters: {
    addPrescriptionPayload: state => _.cloneDeep(state.addPrescriptionPayload),
    prescriptions: state => _.cloneDeep(state.prescriptions),
    medicationCheckList: state => {
      const result = state.medicationsToday.map(x => 
        new MedicationCheckListItem(
          x.name, 
          x.quantity, 
          x.hour, 
          x.minute,
          state.userHistories.findIndex(y => 
            y.name === x.name &&
            y.quantity === x.quantity &&
            y.hour === x.hour && 
            y.minute === x.minute
          ) !== -1
        ));
      return result;
    }
  }
})

// define your own `useStore` composition function
export function useStore () {
  return baseUseStore(key)
}