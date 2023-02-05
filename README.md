# IntervalRecord
The record Interval<T> will make it easy to deal with https://en.wikipedia.org/wiki/Interval_(mathematics). If you have a validity period you no longer need to figure out the exact business logic.

The interval supports Closed, ClosedOpen, OpenClosed and Open boundaryTypes.
```mermaid
gantt
    Reference :crit, a, 2022-01-06, 2022-01-10
    Before :active, b, 2022-01-03, 2022-01-05
    Meets :active, c, 2022-01-04, 2022-01-06
    EndInsideOnly :active, d, 2022-01-05, 2022-01-07
    Starts :active, e, 2022-01-06, 2022-01-08
    ContainedBy :active, f, 2022-01-07, 2022-01-09
    Finishes :active, g, 2022-01-08, 2022-01-10
    Equal :active, h, 2022-01-06, 2022-01-10
    FinishedBy :active, i, 2022-01-05, 2022-01-10
    Contains :active, j, 2022-01-05, 2022-01-11
    StartedBy :active, k, 2022-01-06, 2022-01-11
    StartInsideOnly :active, l, 2022-01-09, 2022-01-11
    MetBy :active, m, 2022-01-10, 2022-01-12
    After :active, n, 2022-01-11, 2022-01-13
```