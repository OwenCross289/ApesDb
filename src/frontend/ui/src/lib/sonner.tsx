import { Toaster as SonnerToaster, type ToasterProps } from "sonner";

import { useTheme } from "./theme";

function Toaster({ ...props }: ToasterProps) {
  const { resolvedMode } = useTheme();

  return (
    <SonnerToaster
      className="toaster group"
      theme={resolvedMode}
      toastOptions={{
        classNames: {
          toast:
            "group toast group-[.toaster]:border-border group-[.toaster]:bg-popover group-[.toaster]:text-popover-foreground group-[.toaster]:shadow-lg",
          description: "group-[.toast]:text-muted-foreground",
          actionButton: "group-[.toast]:bg-primary group-[.toast]:text-primary-foreground",
          cancelButton: "group-[.toast]:bg-muted group-[.toast]:text-muted-foreground",
        },
      }}
      {...props}
    />
  );
}

export { Toaster };
