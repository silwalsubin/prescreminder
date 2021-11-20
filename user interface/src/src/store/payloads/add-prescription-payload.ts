import TimeOfDay from "../domain/time-of-day";
import moment from 'moment';

class AddPrescriptionPayload {
  constructor(){
    this.name = '';
    this.quantity = '';
    this.startDate = moment().toISOString()
    this.timesOfDay = [
      new TimeOfDay()
    ]
  }

  name: string;
  quantity: string;
  startDate: string;
  completeDate?: string;
  expirationDate?: string;
  timesOfDay: TimeOfDay[]
}

export default AddPrescriptionPayload;