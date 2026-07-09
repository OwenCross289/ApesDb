import { useEffect } from "react";
import { Link, createFileRoute, useRouter } from "@tanstack/react-router";
import { Button, ThemeModeSelector, useTheme } from "@apesdb/ui";
import { appName } from "@apesdb/common";
import { useAuth } from "../auth-context";

type LoginSearch = {
  redirect?: string;
};

export const Route = createFileRoute("/login")({
  validateSearch: (search: Record<string, unknown>): LoginSearch => ({
    redirect: isLocalReturnUrl(search.redirect) ? search.redirect : undefined,
  }),
  component: LoginComponent,
});

function GoogleIcon() {
  return (
    <svg className="size-4" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
      <path
        d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z"
        fill="#4285F4"
      />
      <path
        d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z"
        fill="#34A853"
      />
      <path
        d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.07H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.93l2.85-2.22.81-.62z"
        fill="#FBBC05"
      />
      <path
        d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.07l3.66 2.84c.87-2.6 3.3-4.53 6.16-4.53z"
        fill="#EA4335"
      />
    </svg>
  );
}

function LoginComponent() {
  const { isAuthenticated, login } = useAuth();
  const { resolvedMode } = useTheme();
  const router = useRouter();
  const { redirect = "/" } = Route.useSearch();

  useEffect(() => {
    if (isAuthenticated) {
      router.history.push(redirect);
    }
  }, [isAuthenticated, redirect, router.history]);

  if (isAuthenticated) {
    return null;
  }

  return (
    <main className="grid min-h-[calc(100svh-3.5rem)] lg:grid-cols-2">
      <section className="relative flex flex-col justify-center px-6 py-12 lg:px-16 xl:px-24">
        <header className="absolute top-0 left-0 flex w-full items-center justify-between p-6">
          <p className="text-sm font-medium uppercase tracking-wider text-muted-foreground">
            {appName}
          </p>
          <ThemeModeSelector />
        </header>
        <div className="mx-auto w-full max-w-sm space-y-6">
          <div className="space-y-2 text-center">
            <h1 className="text-3xl font-semibold tracking-tight">Welcome</h1>
            <p className="text-muted-foreground">Log in to {appName} to continue.</p>
          </div>
          <Button
            className="w-full"
            onClick={() => login({ connection: "google", returnUrl: redirect })}
            size="lg"
            type="button"
            variant="outline"
          >
            <GoogleIcon />
            Continue with Google
          </Button>
          <p className="text-center text-xs text-muted-foreground">
            By continuing, you agree to {appName}&apos;s Terms of Service and{" "}
            <Link
              to="/privacy"
              className="font-medium text-foreground underline-offset-4 hover:underline"
            >
              Privacy Policy
            </Link>
            .
          </p>
        </div>
      </section>
      <section className="relative hidden bg-muted lg:block">
        <img
          alt={`${resolvedMode} mode preview`}
          className="absolute inset-0 h-full w-full object-cover"
          src={`/${resolvedMode}-login-banner.png`}
        />
      </section>
    </main>
  );
}

function isLocalReturnUrl(value: unknown): value is string {
  return (
    typeof value === "string" &&
    value.startsWith("/") &&
    !value.startsWith("//") &&
    !value.startsWith("/\\")
  );
}
