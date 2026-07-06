import { toast } from "sonner";
import { registerSW } from "virtual:pwa-register";

const updateToastId = "pwa-update-available";

let isRegistered = false;

export function registerPwaUpdateListener() {
  if (isRegistered) {
    return;
  }

  isRegistered = true;

  const updateServiceWorker = registerSW({
    immediate: true,
    onNeedRefresh() {
      toast("New version available", {
        id: updateToastId,
        description: "Refresh to update ApesDb.",
        duration: Number.POSITIVE_INFINITY,
        dismissible: false,
        closeButton: false,
        action: {
          label: "Refresh",
          onClick: () => {
            void updateServiceWorker(true);
          },
        },
      });
    },
  });
}
