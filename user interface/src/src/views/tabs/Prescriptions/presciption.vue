<template>
  <div class="prescription-card">
    <div class="prescription-information">
      <ion-text color="dark">
        <h4>{{prescription.name}}</h4>
      </ion-text>
      <ion-text color="dark">
        <p>{{prescription.quantity}}</p>
      </ion-text>
    </div>
    <div class="prescription-actions">
            <ion-button
        fill="clear"
        color="success"
      >
        <ion-icon :icon="createOutline" />
      </ion-button>
      <ion-button
        fill="clear"
        color="danger"
      >
        <ion-icon :icon="trashOutline" />
      </ion-button>
    </div>
  </div>
</template>

<script>
import PrescriptionViewModal from '@/store/view-models/prescription-view-modal'
import {
  createOutline,
  trashOutline,
} from 'ionicons/icons';

export default {
  components: { },
  props: {
    prescription: { type: PrescriptionViewModal, required: true }
  },
  setup() {
    return {
      createOutline,
      trashOutline,
    }
  }
}
</script>

<style scoped>
.prescription-card {
  /* padding: 5px; */
  /* width: 100%; */
  /* background-color: grey; */
  display: flex;
  margin-bottom: 10px;
  border-radius: 10px;
  border: 1px solid grey;
}

.prescription-information {
  padding-left: 10px;
  flex: 4;
}

.prescription-actions {
  flex: 1;
}
</style>