export default class UserHistoryViewModel {
  constructor(name: string, quantity: string, hour: number, minute: number){
    this.name = name;
    this.quantity = quantity;
    this.hour = hour;
    this.minute = minute;
    this.intakeDate = new Date();
  }

  name: string;
  quantity: string;
  hour: number;
  minute: number;
  intakeDate: Date;
}