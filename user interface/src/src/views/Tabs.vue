<template>
  <ion-page>
    <ion-tabs @ionTabsDidChange="afterTabChange">
      <ion-router-outlet></ion-router-outlet>
      <ion-tab-bar slot="bottom">
        <ion-tab-button tab="tab1" href="/tabs/dashboard" >
          <ion-icon :icon="homeOutline" />
        </ion-tab-button>
          
        <ion-tab-button tab="tab2" href="/tabs/prescriptions" >
          <ion-icon :icon="listCircleOutline" />
        </ion-tab-button>
        
        <ion-tab-button tab="tab4" href="/tabs/account" >
          <ion-icon :icon="personCircle" />
        </ion-tab-button>
      </ion-tab-bar>
    </ion-tabs>
  </ion-page>
</template>

<script lang="ts">
import { IonTabBar, IonTabButton, IonTabs, IonIcon, IonPage, IonRouterOutlet, alertController } from '@ionic/vue';
import {
  homeOutline,
  listCircleOutline,
  personCircle,
} from 'ionicons/icons';
import { onMounted } from "vue";
import { useStore } from '@/store/store';

export default {
  name: 'Tabs',
  components: { IonTabs, IonTabBar, IonTabButton, IonIcon, IonPage, IonRouterOutlet, },
  setup() {
    const store = useStore();
    onMounted(async () => {
      await store.dispatch('loadPrescriptions');

      for(let i=0; i <5; i++){
        const alert = await alertController.create({
          cssClass: 'my-custom-class',
          header: 'Confirm!',
          message: 'Message <strong>text</strong>!!!',
          buttons: [
            {
              text: 'Cancel',
              role: 'cancel',
              cssClass: 'secondary',
              handler: (blah) => {
                console.log('Confirm Cancel:', blah);
              },
            },
            {
              text: 'Okay',
              handler: () => {
                console.log('Confirm Okay');
              },
            },
          ],
        });
        alert.present();
      }     
    })
    const afterTabChange = async () => {
      await store.dispatch('sessionCheck');
    }
    return {
      afterTabChange,
      homeOutline,
      listCircleOutline,
      personCircle,
    }
  }
}
</script>