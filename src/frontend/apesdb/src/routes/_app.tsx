import {
  Link,
  Outlet,
  createFileRoute,
  redirect,
  useCanGoBack,
  useNavigate,
  useRouterState,
} from "@tanstack/react-router";
import { useCallback, useState } from "react";
import {
  Button,
  Separator,
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarHeader,
  SidebarInset,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarProvider,
  SidebarRail,
  SidebarTrigger,
  TooltipProvider,
} from "@apesdb/ui";
import { ArrowLeft, Gamepad2, Home, Settings } from "lucide-react";
import { useAuth } from "../auth-context";
import { AccountMenu } from "../account-menu";
import { AppBreadcrumbs } from "../app-breadcrumbs";
import { NotificationBell } from "../features/notifications/notification-bell";
import { useNotificationStream } from "../features/notifications/use-notification-stream";
import { TeamProvider, useActiveTeam } from "../features/teams/team-context";
import { TeamSwitcher } from "../features/teams/select-team/team-switcher";

export const Route = createFileRoute("/_app")({
  staticData: {
    breadcrumbs: [{ label: "Home", to: "/" }],
  },
  beforeLoad: ({ context, location }) => {
    if (!context.auth.isAuthenticated) {
      throw redirect({
        to: "/login",
        search: {
          redirect: location.href,
        },
        replace: true,
      });
    }
  },
  component: AppLayout,
});

function AppLayout() {
  const [notificationsOpen, setNotificationsOpen] = useState(false);
  const openNotifications = useCallback(() => {
    setNotificationsOpen(true);
  }, []);

  useNotificationStream(openNotifications);

  return (
    <TeamProvider>
      <TooltipProvider>
        <SidebarProvider className="h-svh overflow-hidden">
          <AppSidebar />
          <SidebarInset>
            <header className="flex h-12 shrink-0 items-center gap-2 border-b px-4">
              <SidebarTrigger className="-ml-1" />
              <Separator className="h-4" orientation="vertical" />
              <AppBreadcrumbs />
              <div className="ml-auto flex items-center">
                <NotificationBell open={notificationsOpen} onOpenChange={setNotificationsOpen} />
              </div>
            </header>
            <div className="flex min-h-0 flex-1 flex-col overflow-y-auto p-3">
              <AppBackButton />
              <div className="min-h-0 flex-1">
                <Outlet />
              </div>
            </div>
          </SidebarInset>
        </SidebarProvider>
      </TooltipProvider>
    </TeamProvider>
  );
}

function AppBackButton() {
  const canGoBack = useCanGoBack();
  const navigate = useNavigate();
  const pathname = useRouterState({ select: (state) => state.location.pathname });
  const pathSegments = pathname.split("/").filter(Boolean);
  const goBack = useCallback(() => {
    if (canGoBack) {
      window.history.back();
      return;
    }

    void navigate({ to: "/", replace: true });
  }, [canGoBack, navigate]);

  if (pathSegments.length <= 1) {
    return null;
  }

  return (
    <Button className="mb-3 shrink-0 self-start" type="button" variant="ghost" onClick={goBack}>
      <ArrowLeft />
      Back
    </Button>
  );
}

function AppSidebar() {
  const { user, logout } = useAuth();
  const { activeTeam } = useActiveTeam();
  const pathname = useRouterState({ select: (state) => state.location.pathname });

  return (
    <Sidebar collapsible="icon">
      <SidebarHeader>
        <SidebarMenu>
          <SidebarMenuItem>
            <TeamSwitcher />
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarHeader>
      <SidebarContent>
        <SidebarGroup>
          <SidebarGroupLabel className="text-primary">General</SidebarGroupLabel>
          <SidebarGroupContent>
            <SidebarMenu>
              <SidebarMenuItem>
                <SidebarMenuButton
                  render={<Link to="/" />}
                  isActive={pathname === "/"}
                  tooltip="Home"
                >
                  <Home />
                  <span>Home</span>
                </SidebarMenuButton>
              </SidebarMenuItem>
              <SidebarMenuItem>
                <SidebarMenuButton
                  render={<Link to="/games" />}
                  isActive={pathname.startsWith("/games")}
                  tooltip="Games"
                >
                  <Gamepad2 />
                  <span>Games</span>
                </SidebarMenuButton>
              </SidebarMenuItem>
            </SidebarMenu>
          </SidebarGroupContent>
        </SidebarGroup>
        <SidebarGroup>
          <SidebarGroupLabel className="text-primary">Team</SidebarGroupLabel>
          <SidebarGroupContent>
            <SidebarMenu>
              <SidebarMenuItem>
                {activeTeam !== null ? (
                  <SidebarMenuButton
                    render={<Link params={{ teamId: activeTeam.id }} to="/teams/$teamId/manage" />}
                    isActive={pathname === `/teams/${activeTeam.id}/manage`}
                    tooltip="Manage"
                  >
                    <Settings />
                    <span>Manage</span>
                  </SidebarMenuButton>
                ) : (
                  <SidebarMenuButton disabled tooltip="Manage">
                    <Settings />
                    <span>Manage</span>
                  </SidebarMenuButton>
                )}
              </SidebarMenuItem>
            </SidebarMenu>
          </SidebarGroupContent>
        </SidebarGroup>
      </SidebarContent>
      <SidebarFooter>
        <SidebarMenu>
          <SidebarMenuItem>
            <AccountMenu user={user} onLogout={() => void logout()} />
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarFooter>
      <SidebarRail />
    </Sidebar>
  );
}
