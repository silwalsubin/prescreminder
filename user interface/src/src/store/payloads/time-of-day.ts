import { Guid } from 'guid-typescript';

class TimeOfDay {
  constructor(){
    this.id = Guid.create();
    this.hour = 0;
    this.minute = 0;
  }

  id: Guid;
  hour: number;
  minute: number;
}

export default TimeOfDay;