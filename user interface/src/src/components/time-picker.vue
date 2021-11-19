<template>
  <ion-datetime
    :value="timesOfDay"
    display-format="h:mm a"
    placeholder="Time Of Day"
    @ionBlur="handleTimeOfDayChange($event.target.value)"
    @ionFocus="handleTimeOfDayChange($event.target.value)"
  />
</template>

<script>
import { IonDatetime } from '@ionic/vue';
import { computed } from 'vue'
import * as moment from 'moment';

export default {
  name: 'timePicker',
  components: {
    IonDatetime, 
  },
  props: {
    hour: { type: Number, required: true },
    minute: { type: Number, required: true },
  },
  setup(props, context) {
    const timesOfDay = computed(() => {
      const tempMoment = moment();
      tempMoment.set({hour: props.hour, minute: props.minute});
      return tempMoment.toISOString();
    });

    const handleTimeOfDayChange = (e) => {
      const time = moment(e);
      const hourMinute = {
        hour: time.hour(),
        minute: time.minutes(),
      }
      context.emit('input', hourMinute);
    };

    return {
      handleTimeOfDayChange,
      timesOfDay,
    }
  }
}
</script>