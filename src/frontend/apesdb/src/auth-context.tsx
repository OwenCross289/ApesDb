import { createContext, useCallback, useContext, useEffect, useState, type ReactNode } from "react";

export type AuthUser = {
  id: string;
  email: string;
  name: string;
};

type LoginOptions = {
  returnUrl?: string;
  connection?: string;
};

type AuthContextValue = {
  user: AuthUser | null;
  isLoading: boolean;
  isAuthenticated: boolean;
  login: (options?: LoginOptions) => void;
  logout: () => Promise<void>;
  refresh: () => Promise<void>;
};

const AuthContext = createContext<AuthContextValue | null>(null);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<AuthUser | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  const refresh = useCallback(async () => {
    try {
      const response = await fetch("/api/auth/me", { credentials: "include" });

      if (response.ok) {
        setUser(await response.json());
      } else {
        setUser(null);
      }
    } catch {
      setUser(null);
    } finally {
      setIsLoading(false);
    }
  }, []);

  useEffect(() => {
    refresh();
  }, [refresh]);

  const login = useCallback(({ returnUrl = "/", connection }: LoginOptions = {}) => {
    const params = new URLSearchParams();
    params.set("returnUrl", returnUrl);

    if (connection) {
      params.set("connection", connection);
    }

    window.location.href = `/api/auth/login?${params.toString()}`;
  }, []);

  const logout = useCallback(async () => {
    const response = await fetch("/api/auth/logout", {
      method: "POST",
      credentials: "include",
    });

    if (response.ok) {
      const { logoutUrl }: { logoutUrl: string } = await response.json();
      window.location.href = logoutUrl;
    } else {
      setUser(null);
    }
  }, []);

  const value: AuthContextValue = {
    user,
    isLoading,
    isAuthenticated: user !== null,
    login,
    logout,
    refresh,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth(): AuthContextValue {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider.");
  }

  return context;
}
