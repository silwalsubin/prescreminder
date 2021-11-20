import { Guid } from 'guid-typescript';

class TimeOfDay {
  constructor(){
    this.id = Guid.create().toString();
    this.hour = 0;
    this.minute = 0;
  }

  id: string;
  hour: number;
  minute: number;
}

export default TimeOfDay;