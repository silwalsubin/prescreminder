<template>
  <ion-page>
    <tab-header header-title="Prescreminder"/>
    <ion-content :fullscreen="true">  
      <div class="dashboard-tab-container">
      Dashboard
      </div>
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import { IonPage, IonContent } from '@ionic/vue';
import TabHeader from '@/components/tabs/tab-header.vue'

export default  {
  name: 'DashboardTab',
  components: { IonContent, IonPage, TabHeader, }
}
</script>

<style scoped>
.dashboard-tab-container {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
}
</style>