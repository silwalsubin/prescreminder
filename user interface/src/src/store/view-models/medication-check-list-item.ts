export default class MedicationCheckListItem {
  constructor(name: string, quantity: string, hour: number, minute: number, historyId: string|null){
    this.name = name;
    this.quantity = quantity;
    this.hour = hour;
    this.minute = minute;
    this.historyId = historyId;
  }

  name: string;
  quantity: string;
  hour: number;
  minute: number;
  historyId: string|null;
}