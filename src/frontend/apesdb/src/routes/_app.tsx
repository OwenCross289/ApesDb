import { Link, Outlet, createFileRoute, redirect, useRouterState } from "@tanstack/react-router";
import {
  Avatar,
  AvatarFallback,
  AvatarImage,
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
  ThemeModeSelector,
  TooltipProvider,
} from "@apesdb/ui";
import { appName } from "@apesdb/common";
import { Home, LogOut, UserCircle } from "lucide-react";
import { useAuth } from "../auth-context";

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
    <TooltipProvider>
      <SidebarProvider>
        <AppSidebar />
        <SidebarInset>
          <header className="flex h-12 shrink-0 items-center gap-2 border-b px-4">
            <SidebarTrigger className="-ml-1" />
            <Separator className="h-4" orientation="vertical" />
            <p className="text-sm font-medium">{appName}</p>
          </header>
          <div className="flex flex-1 flex-col p-6">
            <Outlet />
          </div>
        </SidebarInset>
      </SidebarProvider>
    </TooltipProvider>
  );
}

function AppSidebar() {
  const { user, logout } = useAuth();
  const pathname = useRouterState({ select: (state) => state.location.pathname });
  const accountName = user?.name.trim();
  const accountLabel = accountName || "Signed in";
  const accountTooltip = accountName || "Account";

  return (
    <Sidebar collapsible="icon">
      <SidebarHeader>
        <SidebarMenu>
          <SidebarMenuItem>
            <SidebarMenuButton size="lg" tooltip={appName}>
              <div className="flex aspect-square size-8 items-center justify-center rounded-md bg-sidebar-primary text-sidebar-primary-foreground">
                <Home className="size-4" />
              </div>
              <div className="grid flex-1 text-left text-sm leading-tight">
                <span className="truncate font-semibold">{appName}</span>
                <span className="truncate text-xs">Dashboard</span>
              </div>
            </SidebarMenuButton>
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
            </SidebarMenu>
          </SidebarGroupContent>
        </SidebarGroup>
      </SidebarContent>
      <SidebarFooter>
        <div className="px-2 group-data-[collapsible=icon]:hidden">
          <ThemeModeSelector />
        </div>
        <SidebarMenu>
          <SidebarMenuItem>
            <SidebarMenuButton size="lg" tooltip={accountTooltip}>
              <Avatar className="size-8 rounded-md">
                <AvatarImage alt={accountLabel} src={user?.pictureUrl ?? undefined} />
                <AvatarFallback className="rounded-md bg-sidebar-accent text-sidebar-foreground">
                  <UserCircle className="size-4" />
                </AvatarFallback>
              </Avatar>
              <div className="grid flex-1 text-left leading-tight">
                <span className="truncate font-medium">{accountLabel}</span>
              </div>
            </SidebarMenuButton>
          </SidebarMenuItem>
          <SidebarMenuItem>
            <SidebarMenuButton onClick={() => void logout()} tooltip="Log out">
              <LogOut />
              <span>Log out</span>
            </SidebarMenuButton>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarFooter>
      <SidebarRail />
    </Sidebar>
  );
}
