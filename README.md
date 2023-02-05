# IntervalRecord
The record Interval<T> will make it easy to deal with https://en.wikipedia.org/wiki/Interval_(mathematics). If you have a validity period you no longer need to figure out the exact business logic.

The interval supports Closed, ClosedOpen, OpenClosed and Open boundaryTypes.
```mermaid
gantt
    Reference :active, a, 2022-01-06, 2022-01-10
    Before :b, 2022-01-03, 2022-01-05
    Meets :c, 2022-01-04, 2022-01-06
    EndInsideOnly :d, 2022-01-05, 2022-01-07
    Starts :e, 2022-01-06, 2022-01-08
    ContainedBy :f, 2022-01-07, 2022-01-09
    Finishes :g, 2022-01-08, 2022-01-10
    Equal :h, 2022-01-06, 2022-01-10
    FinishedBy :i, 2022-01-05, 2022-01-10
    Contains :j, 2022-01-05, 2022-01-11
    StartedBy :k, 2022-01-06, 2022-01-11
    StartInsideOnly :l, 2022-01-09, 2022-01-11
    MetBy :m, 2022-01-10, 2022-01-12
    After :n, 2022-01-11, 2022-01-13
```