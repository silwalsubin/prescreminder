<template>
  <div class="blackboard">
    <div class="blackboard-message">
      <ion-icon 
        :icon="getIcon" 
        :color="getColor"
        size="large" 
      />
      <br>
      <ion-text class="blackboard-message--text">
      {{message}}
      </ion-text>
    </div>
  </div>
</template>

<script>
import { IonIcon, IonText } from '@ionic/vue';
import {
  alertCircleOutline,
  checkmarkCircleOutline,
} from 'ionicons/icons';
import { computed } from "vue";
export default  {
  components: {
    IonIcon,
    IonText,
  },
  props: {
    message : { type: String, required: true },
    type: { type: String, required: true }
  }, 
  setup(props) {
    const getIcon = computed(() => {
      switch(props.type){
        case "success":
          return checkmarkCircleOutline;
        case "info":
          return alertCircleOutline;
        default: 
          return null;
      }
    })

    const getColor = computed(() => {
      switch(props.type){
        case "success":
          return "success";
        case "info":
          return "primary";
        default: 
          return null;
      }
    })
    return {
      getColor,
      getIcon,
    }
  }
}
</script>

<style scoped>
.blackboard {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
  padding-left: 20px;
  padding-right: 20px;
  text-align: center;
}

.blackboard-message {
  border: 1px solid grey;
  border-radius: 10px;
  padding: 20px;
}
</style>

<style scoped lang="scss">
@import "@/styles/fonts/font-sizes.scss";
@import "@/styles/animations/blinking-animation.scss";

.blackboard-message {
  animation: $blink-default;
}

.blackboard-message--text {
  font-size: $font-size-sm;
}
</style>