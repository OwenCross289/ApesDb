import { type ButtonHTMLAttributes } from "react";
import { cn } from "./cn";

export type ButtonProps = ButtonHTMLAttributes<HTMLButtonElement>;

export function Button({ className, type = "button", ...props }: ButtonProps) {
  return (
    <button
      className={cn(
        "inline-flex h-10 items-center justify-center rounded-md bg-emerald-400 px-4 text-sm font-medium text-zinc-950 transition-colors hover:bg-emerald-300 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-emerald-300 disabled:pointer-events-none disabled:opacity-50",
        className,
      )}
      type={type}
      {...props}
    />
  );
}
