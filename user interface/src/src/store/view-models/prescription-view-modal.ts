interface PrescriptionViewModal {
  prescriptionId: string;
  name: string;
  quantity: string;
  startDate: string;
  completeDate?: string;
  expirationDate?: string;
  timesOfDay: TimeOfDay[];
}

interface TimeOfDay {
  id: string;
  hour: number;
  minute: number;
}

export default PrescriptionViewModal;