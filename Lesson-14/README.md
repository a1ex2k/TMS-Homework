### 14. Многопоточность и параллельное программирование, библиотека TPL

| Method           | DegreeOfParallelism | Mean      | Error     | StdDev    | Allocated |
|----------------- |-------------------- |----------:|----------:|----------:|----------:|
| SumSingleThread  | -                   |  5.316 ms | 0.0130 ms | 0.0058 ms |       5 B |
| SumAsParallel    | 2                   | 38.783 ms | 0.3940 ms | 0.1749 ms |    3581 B |
| SumWithTaskAsync | 2                   |  2.779 ms | 0.0222 ms | 0.0099 ms |     932 B |
| SumWithThreads   | 2                   |  3.316 ms | 0.0640 ms | 0.0381 ms |     627 B |
| SumAsParallel    | 3                   | 27.620 ms | 0.9592 ms | 0.5708 ms |    3981 B |
| SumWithTaskAsync | 3                   |  2.058 ms | 0.1116 ms | 0.0664 ms |    1124 B |
| SumWithThreads   | 3                   |  2.588 ms | 0.0635 ms | 0.0378 ms |     874 B |
| SumAsParallel    | 4                   | 21.370 ms | 0.8165 ms | 0.4859 ms |    4458 B |
| SumWithTaskAsync | 4                   |  1.802 ms | 0.0831 ms | 0.0435 ms |    1314 B |
| SumWithThreads   | 4                   |  2.253 ms | 0.0134 ms | 0.0060 ms |    1123 B |
| SumAsParallel    | 5                   | 20.277 ms | 4.4751 ms | 2.6631 ms |    4535 B |
| SumWithTaskAsync | 5                   |  1.835 ms | 0.1325 ms | 0.0788 ms |    1506 B |
| SumWithThreads   | 5                   |  2.160 ms | 0.0641 ms | 0.0382 ms |    1369 B |
| SumAsParallel    | 6                   | 21.924 ms | 1.1854 ms | 0.7054 ms |    5321 B |
| SumWithTaskAsync | 6                   |  1.984 ms | 0.0588 ms | 0.0307 ms |    1700 B |
| SumWithThreads   | 6                   |  2.425 ms | 0.2120 ms | 0.1109 ms |    1618 B |
| SumAsParallel    | 7                   | 18.441 ms | 0.2195 ms | 0.0975 ms |    5450 B |
| SumWithTaskAsync | 7                   |  1.669 ms | 0.0151 ms | 0.0079 ms |    1892 B |
| SumWithThreads   | 7                   |  2.074 ms | 0.0650 ms | 0.0340 ms |    1866 B |
| SumAsParallel    | 8                   | 15.936 ms | 0.2117 ms | 0.1107 ms |    5898 B |
| SumWithTaskAsync | 8                   |  1.583 ms | 0.0134 ms | 0.0060 ms |    2082 B |
| SumWithThreads   | 8                   |  1.978 ms | 0.0253 ms | 0.0133 ms |    2113 B |
| SumAsParallel    | 9                   | 14.573 ms | 0.2967 ms | 0.1765 ms |    6310 B |
| SumWithTaskAsync | 9                   |  1.533 ms | 0.0127 ms | 0.0066 ms |    2274 B |
| SumWithThreads   | 9                   |  1.949 ms | 0.0559 ms | 0.0332 ms |    2361 B |
| SumAsParallel    | 10                  | 13.405 ms | 0.0114 ms | 0.0050 ms |    6762 B |
| SumWithTaskAsync | 10                  |  1.401 ms | 0.0058 ms | 0.0030 ms |    2466 B |
| SumWithThreads   | 10                  |  1.876 ms | 0.0143 ms | 0.0075 ms |    2611 B |
