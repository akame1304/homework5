class Adversary {
    constructor(m, n, T, N, lambda) {
        this.numberOfSystem = m;
        this.numberOfAttacks = n;
        this.timeInterval = T;
        this.subtimeInterval = N;
        this.lambda = lambda;
        this.probability = lambda * (T / N);

        this.lineChartValuesChart1 = [];
        this.lineChartValuesChart2 = [];
        this.lineChartValuesChart3 = [];
        this.lineChartValuesChart4 = [];

        this.generateAttacks();
    }

    generateAttacks() {
        for (let i = 0; i < this.numberOfSystem; i++) {
            const valuesChart2 = [];

            for (let j = 0; j < this.numberOfAttacks; j++) {
                const randomNumber = Math.random();

                if (randomNumber > this.probability) {
                    // Attack success
                    console.log(`System ${i}: Attack ${j} succeeded`);
                    valuesChart2.push(1);
                } else {
                    // Attack failed
                    console.log(`System ${i}: Attack ${j} failed`);
                    valuesChart2.push(0);
                }
            }
            this.lineChartValuesChart2.push(valuesChart2);
        }
    }

    createChart3Values() {
        for (let i = 0; i < this.lineChartValuesChart2.length; i++) {
            const systemIValues = [0];
            let sum = 0;

            for (let j = 0; j < this.lineChartValuesChart2[i].length; j++) {
                sum += this.lineChartValuesChart2[i][j];
                systemIValues.push(sum / (j + 1));
            }
            this.lineChartValuesChart3.push(systemIValues);
        }
    }

    createChart4Values() {
        for (let i = 0; i < this.lineChartValuesChart2.length; i++) {
            const systemIValues = [0];
            let sum = 0;

            for (let j = 0; j < this.lineChartValuesChart2[i].length; j++) {
                sum += this.lineChartValuesChart2[i][j];
                systemIValues.push(sum / Math.sqrt(j + 1));
            }
            this.lineChartValuesChart4.push(systemIValues);
        }
    }

    createHistoDistrib(values, k) {
        const finalValues = {};

        for (let i = 0; i < values.length; i++) {
            let sum = 0;

            for (let s = 0; s < values[i].length; s++) {
                sum += values[i][s];
            }
            finalValues[i] = sum;
        }

        return finalValues;
    }

    createHistoDistribFloat(values, k) {
        const finalValues = {};

        let i = 0;
        for (const list of values) {
            finalValues[i] = list[list.length - 1];
            i++;
            console.log(`last freq ${i}: ${list[list.length - 1]}`);
        }

        return finalValues;
    }

    getLineChart1AttackList() {
        return this.lineChartValuesChart1;
    }

    getLineChart2AttackList() {
        return this.lineChartValuesChart2;
    }

    getLineChart3AttackList() {
        return this.lineChartValuesChart3;
    }

    getLineChart4AttackList() {
        return this.lineChartValuesChart4;
    }
}
