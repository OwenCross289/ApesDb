import { Link, Outlet, createFileRoute, redirect, useRouterState } from "@tanstack/react-router";
import {
  Separator,
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup,
  SidebarGroupContent,
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
import { Gamepad2, Home } from "lucide-react";
import { useAuth } from "../auth-context";
import { AccountMenu } from "../account-menu";
import { TeamProvider } from "../features/teams/team-context";
import { TeamSwitcher } from "../features/teams/team-switcher";

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
