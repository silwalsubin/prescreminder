interface PrescriptionViewModal {
  prescriptionId: string;
  name: string;
  unitDose: string;
  startDate: string;
  completeDate?: string;
  totalQuantity: number;
  timesOfDay: TimeOfDay[];
}

interface TimeOfDay {
  id: string;
  hour: number;
  minute: number;
}

export default PrescriptionViewModal;