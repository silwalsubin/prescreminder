<template>
  <ion-page>
    <tab-header header-title="History"/>
    <ion-content :fullscreen="true">   
      <div class="history-tab-container">
      History
      </div>
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import { IonPage, IonContent } from '@ionic/vue';
import TabHeader from '@/components/tabs/tab-header.vue'

export default  {
  name: 'HistoryTab',
  components: { IonContent, IonPage, TabHeader }
}
</script>

<style scoped>
.history-tab-container {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
}
</style>