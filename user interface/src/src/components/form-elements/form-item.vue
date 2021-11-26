<template>
  <ion-item lines="full" class="form-item">
    <ion-label position="stacked">
      {{ label }}
    </ion-label>
    <slot/>
  </ion-item>
</template>

<script>
import {
  IonItem,
  IonLabel,
} from '@ionic/vue';

export default {
  components: {
    IonItem,
    IonLabel,
  },
  props: {
    label: { type: String, required: true },
  }
}
</script>

<style scoped>
.form-item {
  --border-radius: 5px;
}
</style>
