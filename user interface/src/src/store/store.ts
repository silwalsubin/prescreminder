import { InjectionKey } from 'vue';
import { createStore, useStore as baseUseStore, Store } from 'vuex';
import httpClient, { fileDownload } from '@/store/http-client';
import { removeBearerToken, setBearerToken } from '@/bearer-token-service';
import AddPrescriptionPayload from './payloads/add-prescription-payload';
import PrescriptionViewModal from './view-models/prescription-view-modal';
import MedicationInfoViewModel from './view-models/medication-info-view-model';
import UserHistoryViewModel from './view-models/user-history-view-model';
import MedicationCheckListItem from './view-models/medication-check-list-item';
import NotificationViewModel from './view-models/notification-view-model';
import _ from 'lodash';


export interface State {
  addPrescriptionPayload: AddPrescriptionPayload;
  prescriptions: PrescriptionViewModal[];
  medicationsToday: MedicationInfoViewModel[];
  userHistories: UserHistoryViewModel[];
  medicationCheckListItems: MedicationCheckListItem[];
  notifications: NotificationViewModel[];
}

export const key: InjectionKey<Store<State>> = Symbol()

export const store = createStore<State>({
  state: {
    addPrescriptionPayload: new AddPrescriptionPayload(),
    prescriptions: [],
    medicationsToday: [],
    userHistories: [],
    medicationCheckListItems: [],
    notifications: []
  },
  actions: {
    logIn(_, payload) {
      return httpClient.post('/user/login', payload).then(response => {
        const bearerToken = response.data.token;
        setBearerToken(bearerToken);
      })
    },
    register(_, payload) {
      return httpClient.post('/user/register', payload);
    },
    deleteAccount() {
      return httpClient.delete('/user').then(() => {
        removeBearerToken();
      });
    },
    loadMedicationsToday({commit}){
      return httpClient.get(`userMedicationToday`).then(response => {
        const userMedications: MedicationCheckListItem[] = response.data;
        return httpClient.get(`userMedicationIntakeHistories/today`)
        .then(res => {
          const historiesToday: UserHistoryViewModel[] = res.data;
          userMedications.forEach(element => {
            const matchingHistory = historiesToday.find(
              x => x.hour === element.hour &&
                x.minute === element.minute &&
                x.prescriptionName === element.name &&
                x.quantity === element.quantity
            );
            element.historyId = matchingHistory ? matchingHistory.historyId : null;
          });
          commit('updateMedicationsToday', userMedications);
        })
      })
    },
    loadNotifications({commit}){
      return httpClient.get('userEventNotifications').then(response => {
        commit('updateNotifications', response.data);
      })
    },
    downloadPrescriptionPdf(){
      return httpClient.get('userPrescription/pdf', {
        responseType: 'blob',        
      }).then(fileDownload)
    },
    getPrescriptionPdf(){
      return httpClient.get('userPrescription/pdf', {
        responseType: 'blob',        
      })
    },
    clearNotification({commit}, notificationId){
      commit('deleteNotification', notificationId);
      return httpClient.post(`userEventNotifications/clear/${notificationId}`);
    },
    loadPrescriptions({commit, dispatch}) {
      return httpClient.get('/userPrescription').then(response => {
        commit('setPrescriptions', response.data);
        dispatch('loadMedicationsToday');
        dispatch('loadNotifications');
      })
    },
    addPrescription({dispatch}, payload) {
      return httpClient.post('/userPrescription/add', payload).then(() => {
        dispatch('loadPrescriptions');
        dispatch('loadMedicationsToday');
        dispatch('loadNotifications');
      });
    },
    deletePrescription({commit, dispatch}, prescriptionId) {
      return httpClient.delete(`/userPrescription/${prescriptionId}`).then(() => {
        commit('deletePrescription', prescriptionId);
        dispatch('loadMedicationsToday');
        dispatch('loadNotifications');
      })
    },
    updatePrescription({commit, dispatch}, payload){
      return httpClient.post(`/userPrescription/${payload.prescriptionId}`, payload.viewModal).then(() => {
        commit('updatePrescription', payload.viewModal);
        dispatch('loadMedicationsToday');
        dispatch('loadNotifications');
      })
    },
    refill({dispatch}, payload) {
      return httpClient.post(`userPrescription/${payload.prescriptionId}/refill/${payload.refill}`).then(() => {
        dispatch('loadPrescriptions');
      })  
    },
    updateMedicationTaken({commit}, payload: MedicationCheckListItem){
      if (payload.historyId) {
        return httpClient.delete(`/userMedicationIntakeHistories/${payload.historyId}`).then(() => {
          payload.historyId = null;
          commit('updateMedicationTaken', payload);
        });
      } else {
        const reqPayload = {
          prescriptionName: payload.name,
          quantity: payload.quantity,
          hour: payload.hour,
          minute: payload.minute,
        }
        return httpClient.post(`/userMedicationIntakeHistories`, reqPayload).then(response => {
          payload.historyId = response.data;
          commit('updateMedicationTaken', payload);
        });
      }
    },
    sessionCheck() {
      return httpClient.get(`/user/sessionCheck`);
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
    updateMedicationsToday(state, payload: MedicationCheckListItem[]){
      state.medicationCheckListItems = payload;
    },
    updateMedicationTaken(state, payload){
      const index = state.medicationCheckListItems.findIndex(y => 
        y.name === payload.name &&
        y.quantity === payload.quantity &&
        y.hour === payload.hour && 
        y.minute === payload.minute
      );
      state.medicationCheckListItems[index].historyId = payload.historyId;
    },
    updateNotifications(state, payload){
      state.notifications = payload;
    },
    deleteNotification(state, notificationId){
      const index = state.notifications.findIndex(x => x.notificationId === notificationId);
      if (index !== -1){
        state.notifications.splice(index, 1);
      }
    }
  },
  getters: {
    addPrescriptionPayload: state => _.cloneDeep(state.addPrescriptionPayload),
    prescriptions: state => _.cloneDeep(state.prescriptions),
    medicationCheckListItems: state => _.cloneDeep(state.medicationCheckListItems),
    notifications: state => _.cloneDeep(state.notifications),
  }
})

// define your own `useStore` composition function
export function useStore () {
  return baseUseStore(key)
}