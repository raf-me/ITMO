#include <bits/stdc++.h>
using namespace std;

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n;
    if (!(cin >> n)) return 0;
    vector<string> a(n + 1);
    for (int i = 1; i <= n; ++i) cin >> a[i];


    vector<int> prevOcc(n + 1), nextOcc(n + 1);
    for (int i = 1, last = 0; i <= n; ++i) {
        if (a[i] != "Empty") last = i;
        prevOcc[i] = last;
    }

    for (int i = n, next = 0; i >= 1; --i) {
        if (a[i] != "Empty") next = i;
        nextOcc[i] = next;
    }

    long long bestVal = -1;
    int bestIdx = -1;
    for (int i = 1; i <= n; ++i) if (a[i] == "Empty") {
        int L = prevOcc[i];
        int R = nextOcc[i];
        long long val = 1LL * (i - L) * (R - i);
        if (val > bestVal || (val == bestVal && i < bestIdx)) {
            bestVal = val;
            bestIdx = i;
        }
    }

    cout << bestIdx << '\n';
    return 0;
}