# IntervalRecord
The record Interval<T> will make it easy to deal with https://en.wikipedia.org/wiki/Interval_(mathematics). If you have a validity period you no longer need to figure out the exact business logic.

The interval supports Closed, ClosedOpen, OpenClosed and Open boundaryTypes.
```mermaid
gantt
    Reference :crit, active, a, 2022-01-06, 2022-01-10

    section Before
    Before :crit, b, 2022-01-03, 2022-01-05

    section Overlaps
    Meets :crit, c, 2022-01-04, 2022-01-06
    Overlaps :crit, d, 2022-01-05, 2022-01-07
    Starts :crit, e, 2022-01-06, 2022-01-08
    ContainedBy :crit, f, 2022-01-07, 2022-01-09
    Finishes :crit, g, 2022-01-08, 2022-01-10
    Equal :crit, h, 2022-01-06, 2022-01-10
    FinishedBy :crit, i, 2022-01-05, 2022-01-10
    Contains :crit, j, 2022-01-05, 2022-01-11
    StartedBy :crit, k, 2022-01-06, 2022-01-11
    OverlappedBy :crit, l, 2022-01-09, 2022-01-11
    MetBy :crit, m, 2022-01-10, 2022-01-12

    section After
    After :crit, n, 2022-01-11, 2022-01-13
```
