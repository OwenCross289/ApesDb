import { Link, Outlet, createFileRoute, redirect, useRouterState } from "@tanstack/react-router";
import {
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
import { appName } from "@apesdb/common";
import { Gamepad2, Home, Settings } from "lucide-react";
import { useAuth } from "../auth-context";
import { AccountMenu } from "../account-menu";
import { NotificationBell } from "../features/notifications/notification-bell";
import { useNotificationStream } from "../features/notifications/use-notification-stream";
import { TeamProvider, useActiveTeam } from "../features/teams/team-context";
import { TeamSwitcher } from "../features/teams/select-team/team-switcher";

export const Route = createFileRoute("/_app")({
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
  useNotificationStream();

  return (
    <TeamProvider>
      <TooltipProvider>
        <SidebarProvider className="h-svh overflow-hidden">
          <AppSidebar />
          <SidebarInset>
            <header className="flex h-12 shrink-0 items-center gap-2 border-b px-4">
              <SidebarTrigger className="-ml-1" />
              <Separator className="h-4" orientation="vertical" />
              <p className="text-sm font-medium">{appName}</p>
              <div className="ml-auto flex items-center">
                <NotificationBell />
              </div>
            </header>
            <div className="flex min-h-0 flex-1 flex-col overflow-y-auto p-3">
              <Outlet />
            </div>
          </SidebarInset>
        </SidebarProvider>
      </TooltipProvider>
    </TeamProvider>
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
