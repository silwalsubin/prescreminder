import { toastController } from '@ionic/vue';

export async function notifyAsync(notificationType: NotificationType, message: string): Promise<void> {
  const toast = await toastController.create({
    message: message,
    duration: 100,
    color: notificationType, 
    animated: true,
    position: 'top',
  });
  toast.present();
}

export enum NotificationType {
  Success = "success",
  Error = "danger",
  Warning = "warning",
  Information = "primary"
}