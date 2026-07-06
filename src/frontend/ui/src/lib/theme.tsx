import * as React from "react";
import { Monitor, Moon, Sun, type LucideIcon } from "lucide-react";

import { Button } from "./button";
import { cn } from "./utils";

export type ThemeMode = "light" | "dark" | "system";

type ResolvedThemeMode = "light" | "dark";

type ThemeContextValue = {
  mode: ThemeMode;
  resolvedMode: ResolvedThemeMode;
  setMode: (mode: ThemeMode) => void;
};

type ThemeProviderProps = {
  children: React.ReactNode;
};

const themeStorageKey = "apesdb-theme";
const systemThemeQuery = "(prefers-color-scheme: dark)";
const ThemeContext = React.createContext<ThemeContextValue | null>(null);

const themeOptions = [
  { value: "light", label: "Light", Icon: Sun },
  { value: "dark", label: "Dark", Icon: Moon },
  { value: "system", label: "System", Icon: Monitor },
] satisfies ReadonlyArray<{
  value: ThemeMode;
  label: string;
  Icon: LucideIcon;
}>;

function canUseDom() {
  return typeof window !== "undefined" && typeof document !== "undefined";
}

function isThemeMode(value: string | null): value is ThemeMode {
  return value === "light" || value === "dark" || value === "system";
}

function getStoredThemeMode(): ThemeMode {
  if (!canUseDom()) {
    return "system";
  }

  try {
    const storedMode = window.localStorage.getItem(themeStorageKey);
    return isThemeMode(storedMode) ? storedMode : "system";
  } catch {
    return "system";
  }
}

function getSystemThemeMode(): ResolvedThemeMode {
  if (!canUseDom()) {
    return "light";
  }

  return window.matchMedia(systemThemeQuery).matches ? "dark" : "light";
}

function resolveThemeMode(mode: ThemeMode): ResolvedThemeMode {
  return mode === "system" ? getSystemThemeMode() : mode;
}

function applyThemeMode(mode: ResolvedThemeMode) {
  if (!canUseDom()) {
    return;
  }

  const root = document.documentElement;
  root.classList.toggle("dark", mode === "dark");
  root.style.colorScheme = mode;
}

function persistThemeMode(mode: ThemeMode) {
  if (!canUseDom()) {
    return;
  }

  try {
    window.localStorage.setItem(themeStorageKey, mode);
  } catch {
    // Ignore unavailable storage, such as private browsing restrictions.
  }
}

export function ThemeProvider({ children }: ThemeProviderProps) {
  const [mode, setMode] = React.useState<ThemeMode>(getStoredThemeMode);
  const [resolvedMode, setResolvedMode] = React.useState<ResolvedThemeMode>(() =>
    resolveThemeMode(getStoredThemeMode()),
  );

  React.useEffect(() => {
    const nextResolvedMode = resolveThemeMode(mode);

    setResolvedMode(nextResolvedMode);
    applyThemeMode(nextResolvedMode);
    persistThemeMode(mode);

    if (!canUseDom() || mode !== "system") {
      return;
    }

    const mediaQuery = window.matchMedia(systemThemeQuery);
    const handleSystemThemeChange = () => {
      const changedResolvedMode = getSystemThemeMode();

      setResolvedMode(changedResolvedMode);
      applyThemeMode(changedResolvedMode);
    };

    mediaQuery.addEventListener("change", handleSystemThemeChange);
    return () => mediaQuery.removeEventListener("change", handleSystemThemeChange);
  }, [mode]);

  const value = React.useMemo(
    () => ({
      mode,
      resolvedMode,
      setMode,
    }),
    [mode, resolvedMode],
  );

  return <ThemeContext.Provider value={value}>{children}</ThemeContext.Provider>;
}

export function useTheme() {
  const context = React.useContext(ThemeContext);

  if (!context) {
    throw new Error("useTheme must be used within a ThemeProvider.");
  }

  return context;
}

type ThemeModeSelectorProps = React.ComponentProps<"div"> & {
  buttonClassName?: string;
  buttonSize?: React.ComponentProps<typeof Button>["size"];
};

export function ThemeModeSelector({
  buttonClassName,
  buttonSize = "sm",
  className,
  ...props
}: ThemeModeSelectorProps) {
  const { mode, setMode } = useTheme();

  return (
    <div
      className={cn(
        "flex items-center gap-1 rounded-sm border border-border bg-muted p-1",
        className,
      )}
      {...props}
    >
      {themeOptions.map(({ value, label, Icon }) => (
        <Button
          aria-pressed={mode === value}
          className={buttonClassName}
          key={value}
          onClick={() => setMode(value)}
          size={buttonSize}
          type="button"
          variant={mode === value ? "default" : "ghost"}
        >
          <Icon data-icon="inline-start" />
          {label}
        </Button>
      ))}
    </div>
  );
}
