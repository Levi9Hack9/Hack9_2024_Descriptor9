import { useEffect, useMemo, useRef, useState } from "react";

interface DropdownOptionsProps {
  options: string[];
  highlightedIndex: number;
  selectedIndex: number;
  eventListeners: {
    onOptionHover: (e: React.MouseEvent<HTMLLIElement>) => void;
    onOptionClick: (e: React.MouseEvent<HTMLLIElement>) => void;
  };
}

interface DropdownProps {
  options: string[];
  onSelect: (category: string) => void;
  id: string;
  className: string;
  preselectedIndex: number;
}

const DropdownOptions: React.FC<DropdownOptionsProps> = ({
  options,
  highlightedIndex,
  selectedIndex,
  eventListeners,
}) => {
  const { onOptionHover, onOptionClick } = eventListeners;
  return (
    <>
      {options.map((el, i) => {
        return (
          <li
            key={i}
            aria-selected={i === selectedIndex}
            data-value={el}
            onMouseOver={(e) => {
              onOptionHover(e);
            }}
            onClick={(e) => {
              onOptionClick(e);
            }}
            className={"option" + (i === highlightedIndex ? " highlight" : "")}
            role="option"
          >
            {el}
          </li>
        );
      })}
    </>
  );
};

export const Dropdown: React.FC<DropdownProps> = ({
  options,
  onSelect,
  id,
  className,
  preselectedIndex,
  ...props
}) => {
  const optList = useRef<any>();
  const selectElement = useRef<any>();
  const [isActive, setIsActive] = useState(false);

  const defaultIndex = 0;
  const [highlightedIndex, setHighlightedIndex] = useState(0);
  const [selectedIndex, setSelectedIndex] = useState(defaultIndex);

  useEffect(() => {
    setSelectedIndex(preselectedIndex);

    if (!preselectedIndex) {
      setSelectedIndex(defaultIndex);
    }
  }, [preselectedIndex]);

  const onOptionHover = (e: React.MouseEvent<HTMLLIElement>) => {
    const element = e.currentTarget;
    const index = Array.from(optList.current!["children"]).findIndex(
      (el) => el === element
    );
    setHighlightedIndex(index);
  };
  const onOptionClick = (e: React.MouseEvent<HTMLLIElement>) => {
    const element = e.currentTarget;
    const category = optList.current!["children"];
    const index = Array.from(category).findIndex((el) => el === element);
    setSelectedIndex(index);
    setIsActive(false);
    onSelect(options[index]);
  };

  const onDropdownFocus = () => {
    setIsActive(true);
  };
  const onDropdownBlur = () => {
    setIsActive(false);
  };
  const onDropdownClick = () => {
    setIsActive(!isActive);
  };

  const getSelectedOptionValue = () => {
    return options[selectedIndex];
  };
  const getSelectedOptionText = () => {
    return options[selectedIndex];
  };

  useEffect(() => {
    selectElement.current["selectedIndex"] = selectedIndex;
    const event = new Event("change", { bubbles: true });
    selectElement.current["dispatchEvent"](event);
  }, [selectedIndex]);
  const select = useMemo(() => {
    return (
      <select {...props} onChange={(props as any).onChange} ref={selectElement}>
        {options.map((el, i) => {
          return (
            <option key={i} value={el}>
              {el}
            </option>
          );
        })}
      </select>
    );
  }, [options, props, selectElement]);

  return (
    <div
      onFocus={onDropdownFocus}
      onBlur={onDropdownBlur}
      onClick={onDropdownClick}
      className={
        "react-dropdown" + (isActive ? " active" : "") + " " + className
      }
      role="listbox"
      tabIndex={0}
      data-value={getSelectedOptionValue()}
      id={id}
    >
      <span className="value">{getSelectedOptionText()}</span>
      <ul
        ref={optList}
        className={"optList" + (isActive ? "" : " hidden")}
        role="presentation"
      >
        <DropdownOptions
          options={options}
          selectedIndex={selectedIndex}
          highlightedIndex={highlightedIndex}
          eventListeners={{ onOptionHover, onOptionClick }}
        />
      </ul>
      {select}
    </div>
  );
};
