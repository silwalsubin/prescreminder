export default class MedicationCheckListItem {
  constructor(name: string, quantity: string, hour: number, minute: number, taken: boolean){
    this.name = name;
    this.quantity = quantity;
    this.hour = hour;
    this.minute = minute;
    this.taken = taken;
  }

  name: string;
  quantity: string;
  hour: number;
  minute: number;
  taken: boolean;
}