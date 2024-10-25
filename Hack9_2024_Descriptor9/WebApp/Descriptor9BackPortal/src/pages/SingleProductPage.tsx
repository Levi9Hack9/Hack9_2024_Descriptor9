import React, { useEffect, useState } from "react";
import { Product } from "../types/Product";
import { Dropdown } from "../components/layout/Dropdown";
import { aiDescriptor, saveProduct } from "../services/ProductService";
import { SaveProductData } from "../types/SaveProductData";
import AIAssistance from "../components/AIAssistance";
import { useNavigate, useSearchParams } from "react-router-dom";
import TextArea from "../components/layout/TextArea";
import ConfirmationModal from "../components/modals/ConfirmationModal";

interface SingleProductPageProps {
  products: Product[];
  onSubmit: () => void;
}

interface UploadedImage {
  preview: string;
  raw: Blob | null;
}

interface ErrorForm {
  title: boolean;
  image: boolean;
}

const mapCategories = (products: Product[]) => {
  const categories = products.map((product) => {
    return product.category;
  });
  return [...new Set(categories)];
};

export const SingleProductPage: React.FC<SingleProductPageProps> = ({
  products,
  onSubmit,
}) => {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const queryParamId = searchParams.get("id");

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

  const categories = mapCategories(products);

  const [category, setCategroy] = React.useState<string>("");

  const [title, setTitle] = React.useState<string>("");
  const [english, setEnglish] = React.useState<string>("");
  const [dutch, setDutch] = React.useState<string>("");
  const [spanish, setSpanish] = React.useState<string>("");
  const [romanian, setRomanian] = React.useState<string>("");
  const [ukrainian, setUkrainian] = React.useState<string>("");
  const [serbian, setSerbian] = React.useState<string>("");

  const [isError, setIsError] = useState<ErrorForm>({
    title: true,
    image: true,
  });

  const [isRequestInProgress, setIsRequestInProgress] =
    React.useState<boolean>(false);

  const handleNavigation = () => {
    navigate("/");
  };

  useEffect(() => {
    if (!category) {
      setCategroy(categories[0]);
    }
  }, [category, categories]);

  useEffect(() => {
    const setData = async () => {
      if (!queryParamId) return;

      const product = products.find(
        (product) => product.id.toString() === queryParamId
      );

      if (product) {
        setTitle(product.title);
        setCategroy(product.category);
        setEnglish(product.descriptionTranslations.english);
        setDutch(product.descriptionTranslations.dutch);
        setSpanish(product.descriptionTranslations.spanish);
        setRomanian(product.descriptionTranslations.romanian);
        setUkrainian(product.descriptionTranslations.ukrainian);
        setSerbian(product.descriptionTranslations.serbian);

        const response = await fetch(product.imageUrl);
        const blob = await response.blob();
        setImage({ preview: product.imageUrl, raw: blob });

        setIsError(() => {
          return { image: false, title: false };
        });
      }
    };

    setData();
  }, [queryParamId, searchParams, products]);

  const [image, setImage] = useState<UploadedImage>({ preview: "", raw: null });

  const handleTitleChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    const value = e.target.value;
    setTitle(value);
    setIsError((prev) => {
      return { ...prev, title: !!!value };
    });
  };

  const handleEnglishChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    setEnglish(() => e.target.value);
  };

  const handleDutchChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    setDutch(() => e.target.value);
  };

  const handleSpanishChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    setSpanish(() => e.target.value);
  };

  const handleRomanianChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    setRomanian(() => e.target.value);
  };

  const handleukrainianChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    setUkrainian(() => e.target.value);
  };

  const handleSerbianChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    setSerbian(() => e.target.value);
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const hasFile = e.target?.files;
    if (hasFile?.length) {
      setImage({
        preview: URL.createObjectURL(hasFile[0]),
        raw: hasFile[0],
      });
    }
    setIsError((prev) => {
      return { ...prev, image: !!!hasFile?.length };
    });
  };

  const handleOnSelect = (category: string) => {
    setCategroy(() => category);
  };

  const preparedData = () => {
    const product: SaveProductData = {
      title,
      category,
      descriptionTranslations: {
        english,
        dutch,
        spanish,
        romanian,
        ukrainian,
        serbian,
      },
    };

    const formData = new FormData();
    formData.append("Json", JSON.stringify(product));
    formData.append("File", image.raw!);
    return formData;
  };

  const onAIClick = async () => {
    if (isError.title || isError.image) return;

    if (english || dutch || spanish || romanian || ukrainian || serbian) {
      openModal();
    } else {
      updateDescriptionAndData();
    }
  };

  const updateDescriptionAndData = async () => {
    setIsRequestInProgress(() => true);

    const formData = preparedData();
    const result = await aiDescriptor(formData);
    const { english, dutch, spanish, romanian, ukrainian, serbian } =
      result.descriptionTranslations;

    setEnglish(english);
    setDutch(dutch);
    setSpanish(spanish);
    setRomanian(romanian);
    setUkrainian(ukrainian);
    setSerbian(serbian);
    setIsRequestInProgress(() => false);
  };

  const handleSave = async (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();

    if (isError.title || isError.image || queryParamId) return;
    const formData = preparedData();
    await saveProduct(formData);
    onSubmit();
    handleNavigation();
  };

  const openModal = () => {
    setIsModalOpen(true);
  };

  const onConfirm = async () => {
    await updateDescriptionAndData();
    setIsModalOpen(false);
  };

  const onCancel = () => {
    setIsModalOpen(false);
  };

  return (
    <div className="add-product-container">
      <div className="left-side">
        <div className="row">
          <TextArea
            id="title"
            value={title}
            type="text"
            placeholder="enter title"
            name="title"
            onChange={handleTitleChange}
            className={"clean-slide " + (isError.title ? "error-field" : "")}
            rows={1}
            labelStyle="rows-1"
          />
        </div>
        <div className="row">
          <span>
            <Dropdown
              className="clean-slide no-indent"
              id={"category"}
              options={categories}
              onSelect={handleOnSelect}
              preselectedIndex={categories.indexOf(category)}
            />
            <label htmlFor={category} className="z-index12">
              Category
            </label>
          </span>
        </div>
        <div className="row">
          <TextArea
            id="english"
            value={english}
            type="text"
            placeholder="enter english"
            name="EN"
            onChange={handleEnglishChange}
            className="clean-slide"
          />
        </div>
        <div className="row">
          <TextArea
            id="spanish"
            value={spanish}
            type="text"
            placeholder="enter spanish"
            name="ES"
            onChange={handleSpanishChange}
            className="clean-slide"
          />
        </div>
        <div className="row">
          <TextArea
            id="dutch"
            value={dutch}
            type="text"
            placeholder="enter dutch"
            name="NL"
            onChange={handleDutchChange}
            className="clean-slide"
          />
        </div>
        <div className="row">
          <TextArea
            id="romanian"
            value={romanian}
            type="text"
            placeholder="enter romanian"
            name="RO"
            onChange={handleRomanianChange}
            className="clean-slide"
          />
        </div>
        <div className="row">
          <TextArea
            id="ukrainian"
            value={ukrainian}
            type="text"
            placeholder="enter ukrainian"
            name="UK"
            onChange={handleukrainianChange}
            className="clean-slide"
          />
        </div>
        <div className="row">
          <TextArea
            id="serbian"
            value={serbian}
            type="text"
            placeholder="enter serbian"
            name="SR"
            onChange={handleSerbianChange}
            className="clean-slide"
          />
        </div>
      </div>
      <div className="right-side">
        <div
          className={
            "right-side-div " + (isError.image ? "upload-image-error" : "")
          }
        >
          <label htmlFor="upload-button">
            {image.preview ? (
              <img src={image.preview} alt="preview" width="300" height="300" />
            ) : (
              <>
                <span className="fa-stack fa-2x mt-3 mb-2">
                  <i className="fas fa-circle fa-stack-2x" />
                  <i className="fas fa-store fa-stack-1x fa-inverse" />
                </span>
                <h5 className="text-center">Upload your photo</h5>
              </>
            )}
          </label>
          <input
            type="file"
            id="upload-button"
            style={{ display: "none" }}
            onChange={handleFileChange}
          />
        </div>
        {!queryParamId && (
          <button
            className="save-button"
            onClick={handleSave}
            disabled={isError.title || isError.image}
          >
            SAVE PRODUCT
          </button>
        )}
        {!isError.title && !isError.image && (
          <AIAssistance onClick={onAIClick} />
        )}
      </div>
      <ConfirmationModal
        isModalOpen={isModalOpen}
        onConfirm={onConfirm}
        onCancel={onCancel}
      />
      {isRequestInProgress && (
        <div className="backdrop">
          <span className="loader"></span>
        </div>
      )}
    </div>
  );
};
