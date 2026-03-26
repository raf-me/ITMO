import yaml
from src.data.loader import DatasetLoader, DatasetConfig
from src.evaluation.evaluator import Evaluator
from src.experiments.experiment import Experiment
from src.factories.vectorizer_factory import VectorizerFactory
from src.factories.model_factory import ModelFactory
from src.results.writer import ResultsWriter


def main() -> None:
    with open("config/experiment.yaml", "r", encoding="utf-8") as f:
        cfg = yaml.safe_load(f)

    seed = int(cfg["seed"])

    ds_cfg = DatasetConfig(
        provider=cfg["dataset"]["provider"],
        name=cfg["dataset"]["name"],
        text_field=cfg["dataset"]["text_field"],
        label_field=cfg["dataset"]["label_field"],
        max_train_samples=cfg["dataset"].get("max_train_samples"),
        max_test_samples=cfg["dataset"].get("max_test_samples"),
        seed=seed,
    )

    train_ds, test_ds = DatasetLoader(ds_cfg).load()

    evaluator = Evaluator()
    experiment = Experiment(evaluator=evaluator)

    results = []
    for v_cfg in cfg["vectorizers"]:
        vectorizer = VectorizerFactory.build(v_cfg)

        for m_cfg in cfg["models"]:
            classifier = ModelFactory.build(m_cfg, seed=seed)

            allowed = m_cfg.get("allowed_vectorizers")
            if allowed is not None and v_cfg["type"] not in allowed:
                continue

            run_result = experiment.run(
                vectorizer=vectorizer,
                classifier=classifier,
                train=train_ds,
                test=test_ds,
                seed=seed,
            )

            results.append({
                "dataset": ds_cfg.name,
                "vectorizer": v_cfg["type"],
                "model": m_cfg["type"],
                **run_result,
            })

            print(f"[OK] {v_cfg['type']} + {m_cfg['type']} => "
                  f"acc={run_result['accuracy']:.4f}, f1={run_result['f1_weighted']:.4f}")

    out_path = cfg["output"]["results_csv_path"]
    ResultsWriter(out_path).write(results)
    print(f"\nSaved results to: {out_path}")


if __name__ == "__main__":
    main()