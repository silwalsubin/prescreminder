import TimeOfDay from "../domain/time-of-day";
import moment from 'moment';

class AddPrescriptionPayload {
  constructor(){
    this.name = '';
    this.unitDose = '';
    this.startDate = moment().toISOString()
    this.timesOfDay = [
      new TimeOfDay(0, 0)
    ]
  }

  name: string;
  unitDose: string;
  startDate: string;
  completeDate?: string;
  totalQuantity?: number;
  timesOfDay: TimeOfDay[]
}

export default AddPrescriptionPayload;