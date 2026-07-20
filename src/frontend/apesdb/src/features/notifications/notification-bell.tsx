import {
  Avatar,
  AvatarFallback,
  AvatarImage,
  Badge,
  Button,
  Popover,
  PopoverContent,
  PopoverDescription,
  PopoverHeader,
  PopoverTitle,
  PopoverTrigger,
  Skeleton,
} from "@apesdb/ui";
import { Bell, Check, CheckCheck, Inbox, RefreshCw, User, Users, X } from "lucide-react";
import { formatDateTime } from "../../lib/date";
import type { Notification } from "./notifications.schemas";
import { useMarkNotificationsRead } from "./use-mark-notifications-read";
import { useNotifications } from "./use-notifications";
import { useRespondToInvite } from "./use-respond-to-invite";
import { useTeamInvite } from "./use-team-invite";

type NotificationBellProps = {
  open: boolean;
  onOpenChange: (open: boolean) => void;
};

function errorMessage(error: unknown): string {
  if (error instanceof Error) {
    return error.message;
  }

  return "An unexpected error occurred.";
}

function NotificationSkeleton() {
  return (
    <div className="flex items-start gap-3 p-2">
      <Skeleton className="size-8 rounded-full" />
      <div className="grid flex-1 gap-2">
        <Skeleton className="h-3.5 w-3/4" />
        <Skeleton className="h-3 w-1/3" />
      </div>
    </div>
  );
}

function TeamInviteRow({ notification }: { notification: Notification }) {
  const invite = useTeamInvite(notification.resourceId, notification.isActionable);
  const respond = useRespondToInvite();

  if (invite.isLoading) {
    return <NotificationSkeleton />;
  }

  if (invite.data === null || invite.data === undefined) {
    return (
      <div className="flex items-start gap-3 p-2">
        <Avatar className="size-8">
          <AvatarFallback className="bg-muted text-muted-foreground">
            <Users className="size-3.5" />
          </AvatarFallback>
        </Avatar>
        <div className="grid min-w-0 flex-1 gap-1">
          <p className="text-xs text-muted-foreground">
            {invite.error === null ? "Team invitation" : errorMessage(invite.error)}
          </p>
          <p className="text-[0.625rem] text-muted-foreground">
            {formatDateTime(notification.createdAt)}
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="flex items-start gap-3 p-2">
      <Avatar className="size-8">
        <AvatarImage
          alt={invite.data.invitedBy.name}
          src={invite.data.invitedBy.pictureUrl ?? undefined}
        />
        <AvatarFallback className="bg-muted text-muted-foreground">
          <User className="size-3.5" />
        </AvatarFallback>
      </Avatar>
      <div className="grid min-w-0 flex-1 gap-1">
        <p className="text-xs">
          <span className="font-medium">{invite.data.invitedBy.name}</span> invited you to join{" "}
          <span className="font-medium">{invite.data.team.name}</span>
        </p>
        <p className="text-[0.625rem] text-muted-foreground">
          {formatDateTime(notification.createdAt)}
        </p>
        {notification.isActionable ? (
          <div className="flex gap-2 pt-1">
            <Button
              disabled={respond.isPending}
              onClick={() => respond.mutate({ inviteId: notification.resourceId, accept: true })}
              size="xs"
              type="button"
            >
              <Check data-icon="inline-start" />
              Accept
            </Button>
            <Button
              disabled={respond.isPending}
              onClick={() => respond.mutate({ inviteId: notification.resourceId, accept: false })}
              size="xs"
              type="button"
              variant="outline"
            >
              <X data-icon="inline-start" />
              Decline
            </Button>
          </div>
        ) : null}
        {respond.isError ? (
          <p className="text-[0.625rem] text-destructive">{errorMessage(respond.error)}</p>
        ) : null}
      </div>
      {notification.isUnread ? (
        <span aria-label="Unread" className="mt-1.5 size-2 shrink-0 rounded-full bg-primary" />
      ) : null}
    </div>
  );
}

function NotificationRow({ notification }: { notification: Notification }) {
  if (notification.type === "TeamInvite") {
    return <TeamInviteRow notification={notification} />;
  }

  return (
    <div className="flex items-start gap-3 p-2">
      <div className="grid min-w-0 flex-1 gap-1">
        <p className="text-xs">You have a new notification.</p>
        <p className="text-[0.625rem] text-muted-foreground">
          {formatDateTime(notification.createdAt)}
        </p>
      </div>
      {notification.isUnread ? (
        <span aria-label="Unread" className="mt-1.5 size-2 shrink-0 rounded-full bg-primary" />
      ) : null}
    </div>
  );
}

export function NotificationBell({ open, onOpenChange }: NotificationBellProps) {
  const notifications = useNotifications();
  const markRead = useMarkNotificationsRead();

  const attentionCount = notifications.data?.metadata.attentionCount ?? 0;
  const unreadCount = notifications.data?.metadata.unreadCount ?? 0;
  const items = notifications.data?.items ?? [];

  return (
    <Popover open={open} onOpenChange={onOpenChange}>
      <PopoverTrigger
        render={
          <Button
            aria-label={`Notifications${attentionCount > 0 ? ` (${attentionCount} need attention)` : ""}`}
            className="relative"
            size="icon-lg"
            type="button"
            variant="ghost"
          />
        }
      >
        <Bell />
        {attentionCount > 0 ? (
          <Badge className="absolute -top-1.5 -right-1.5 h-4 min-w-4 px-1">
            {attentionCount > 99 ? "99+" : attentionCount}
          </Badge>
        ) : null}
      </PopoverTrigger>
      <PopoverContent align="end" className="w-80" sideOffset={8}>
        <PopoverHeader className="flex-row items-start justify-between gap-2">
          <div className="grid gap-1">
            <PopoverTitle>Notifications</PopoverTitle>
            <PopoverDescription>
              {items.length === 0 ? "Nothing needs your attention." : "Your latest team activity."}
            </PopoverDescription>
          </div>
          {unreadCount > 0 ? (
            <Button
              disabled={markRead.isPending}
              onClick={() => markRead.mutate()}
              size="xs"
              type="button"
              variant="ghost"
            >
              <CheckCheck data-icon="inline-start" />
              Mark all read
            </Button>
          ) : null}
        </PopoverHeader>
        {notifications.isLoading ? (
          <div className="grid gap-1">
            <NotificationSkeleton />
            <NotificationSkeleton />
          </div>
        ) : null}
        {!notifications.isLoading && notifications.error !== null ? (
          <div className="grid justify-items-center gap-2 py-4 text-center">
            <p className="text-xs text-muted-foreground">{notifications.error}</p>
            <Button onClick={notifications.retry} size="xs" type="button" variant="outline">
              <RefreshCw data-icon="inline-start" />
              Retry
            </Button>
          </div>
        ) : null}
        {!notifications.isLoading && notifications.error === null && items.length === 0 ? (
          <div className="grid justify-items-center gap-2 py-6 text-center">
            <Inbox className="size-5 text-muted-foreground" />
            <p className="text-xs text-muted-foreground">You&apos;re all caught up.</p>
          </div>
        ) : null}
        {items.length > 0 ? (
          <div className="-mx-1 grid max-h-80 gap-0.5 overflow-y-auto">
            {items.map((notification) => (
              <NotificationRow key={notification.id} notification={notification} />
            ))}
          </div>
        ) : null}
      </PopoverContent>
    </Popover>
  );
}
