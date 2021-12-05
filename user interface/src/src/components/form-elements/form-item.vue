<template>
  <ion-item lines="full" class="base-form-item">
    <slot/>
  </ion-item>
</template>

<script>
import { IonItem, } from '@ionic/vue';
export default {
  components: {
    IonItem,
  }
}
</script>

<style scoped>
.base-form-item {
  --border-radius: 5px;
  --inner-padding-top: 15px;
}
</style>