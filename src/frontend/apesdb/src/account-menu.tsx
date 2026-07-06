import type { ReactElement } from "react";
import {
  Avatar,
  AvatarFallback,
  AvatarImage,
  Button,
  Popover,
  PopoverContent,
  PopoverDescription,
  PopoverHeader,
  PopoverTitle,
  PopoverTrigger,
  Sheet,
  SheetContent,
  SheetDescription,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
  SidebarMenuButton,
  ThemeModeSelector,
  useSidebar,
} from "@apesdb/ui";
import { LogOut, UserCircle } from "lucide-react";

import type { AuthUser } from "./auth-context";

type AccountMenuProps = {
  user: AuthUser | null;
  onLogout: () => void;
};

export function AccountMenu({ user, onLogout }: AccountMenuProps) {
  const { isMobile } = useSidebar();
  const accountName = user?.name.trim();
  const accountLabel = accountName || "Signed in";
  const accountEmail = user?.email.trim();
  const accountDescription = accountEmail || "Account preferences";

  if (isMobile) {
    return (
      <Sheet>
        <AccountMenuButton accountLabel={accountLabel} render={<SheetTrigger />} user={user} />
        <SheetContent side="bottom" className="rounded-t-lg">
          <SheetHeader className="sr-only">
            <SheetTitle>{accountLabel}</SheetTitle>
            <SheetDescription>{accountDescription}</SheetDescription>
          </SheetHeader>
          <div className="px-6 pb-6">
            <AccountMenuContent
              accountDescription={accountDescription}
              accountLabel={accountLabel}
              onLogout={onLogout}
              user={user}
            />
          </div>
        </SheetContent>
      </Sheet>
    );
  }

  return (
    <Popover>
      <AccountMenuButton accountLabel={accountLabel} render={<PopoverTrigger />} user={user} />
      <PopoverContent side="right" align="end" sideOffset={8}>
        <PopoverHeader className="sr-only">
          <PopoverTitle>{accountLabel}</PopoverTitle>
          <PopoverDescription>{accountDescription}</PopoverDescription>
        </PopoverHeader>
        <AccountMenuContent
          accountDescription={accountDescription}
          accountLabel={accountLabel}
          onLogout={onLogout}
          user={user}
        />
      </PopoverContent>
    </Popover>
  );
}

function AccountMenuButton({
  accountLabel,
  render,
  user,
}: {
  accountLabel: string;
  render: ReactElement;
  user: AuthUser | null;
}) {
  return (
    <SidebarMenuButton render={render} size="lg" tooltip={accountLabel}>
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
  );
}

function AccountMenuContent({
  accountDescription,
  accountLabel,
  onLogout,
  user,
}: {
  accountDescription: string;
  accountLabel: string;
  onLogout: () => void;
  user: AuthUser | null;
}) {
  return (
    <div className="grid gap-4">
      <div className="flex min-w-0 items-center gap-3">
        <Avatar className="size-9 rounded-md">
          <AvatarImage alt={accountLabel} src={user?.pictureUrl ?? undefined} />
          <AvatarFallback className="rounded-md bg-muted text-muted-foreground">
            <UserCircle className="size-4" />
          </AvatarFallback>
        </Avatar>
        <div className="grid min-w-0 flex-1 leading-tight">
          <p className="truncate text-sm font-medium">{accountLabel}</p>
          <p className="truncate text-xs text-muted-foreground">{accountDescription}</p>
        </div>
      </div>
      <div className="grid gap-2">
        <p className="text-xs font-medium text-muted-foreground">Theme</p>
        <ThemeModeSelector
          aria-label="Theme"
          buttonClassName="w-full justify-center"
          className="grid grid-cols-3 bg-muted/60"
        />
      </div>
      <Button className="w-full justify-start" onClick={onLogout} type="button" variant="ghost">
        <LogOut data-icon="inline-start" />
        Log out
      </Button>
    </div>
  );
}
