import type { DataViewMode } from "@apesdb/ui";
import { useLocalStorage } from "usehooks-ts";

const defaultTableViewStorageKey = "apesdb-default-table-view";
const pageTableViewStorageKeyPrefix = "apesdb-table-view";

function isDataViewMode(value: unknown): value is DataViewMode {
  return value === "rows" || value === "grid";
}

function parseStoredValue(value: string): unknown {
  try {
    return JSON.parse(value);
  } catch {
    return null;
  }
}

function deserializeDefaultTableView(value: string): DataViewMode {
  const parsed = parseStoredValue(value);
  if (isDataViewMode(parsed)) {
    return parsed;
  }

  return "rows";
}

function deserializePageTableView(value: string): DataViewMode | null {
  const parsed = parseStoredValue(value);
  if (isDataViewMode(parsed)) {
    return parsed;
  }

  return null;
}

export function useDefaultTableViewPreference() {
  return useLocalStorage<DataViewMode>(defaultTableViewStorageKey, "rows", {
    deserializer: deserializeDefaultTableView,
  });
}

export function usePageTableViewPreference(pageKey: string) {
  const [defaultView] = useDefaultTableViewPreference();
  const [pageView, setPageView] = useLocalStorage<DataViewMode | null>(
    `${pageTableViewStorageKeyPrefix}:${pageKey}`,
    null,
    { deserializer: deserializePageTableView },
  );

  return [pageView ?? defaultView, setPageView] as const;
}
